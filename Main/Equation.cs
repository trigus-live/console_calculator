using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Main
{
    internal class Equation
    {
        double solution = 0f;
        string content;

        public Equation(string content)
        {
            this.content = content;
        }

        /*
         * 0. remove leading '(' and trailing ')'
         * 1. create a new equation for every bracket and replace the result
         * 2. merge to consecutive signs to either '+' or '-'
         * 3. split before and after every connector (* & /)
         * 4. split before every sign, thats not at the beginning of the part
         * 5. replace every connector with the result of the operation with the previous and next number
         * 6. add every number together
         */
        public bool Solve()
        {
            // remove leading '(' and trailing ')' (Step 0)
            content = content.Remove(0, 1);
            content = content.Remove(content.Length - 1, 1);


            // Step 1 - create a new equation for every bracket and replace the result
            int bracketCount = 0;
            string newEquationContent = "";

            for(int i = 0; i < content.Length; i++)
            {
                char currentChar = content[i];

                if (currentChar == '(')
                    bracketCount++;

                if (bracketCount > 0)
                {
                    // this is inside a bracket and should be added to the new (internal) equation
                    // the whole bracket should be replaced with the solution in the original equation
                    newEquationContent += currentChar;
                    content = content.Remove(i, 1);
                    i--;
                }

                if (currentChar == ')')
                {
                    bracketCount--;
                    if(bracketCount == 0)
                    {
                        // the internal bracket ended and the internal equation can be solved
                        Equation internalEquation = new Equation(newEquationContent);

                        bool success = internalEquation.Solve();
                        if(!success)
                            return false;

                        // add the solution of the internal equation to the original equation
                        content = content.Insert(i + 1, internalEquation.GetSolution().ToString());

                        // update the pointer (not necessary, but saves resources)
                        i = i + internalEquation.GetSolution().ToString().Length;

                        // reset
                        newEquationContent = "";
                    }
                }

            }
            // Step 1 done
            // Step 2 - merge to consecutive signs to either '+' or '-'

            for (int i = 0; i < content.Length; i++)
            {
                // if this check is true, we got the last char
                if (content.Length == i + 1) continue;

                char currentChar = content[i];
                char nextChar = content[i + 1];
                // both currentChar and nextChar should be either '-' or '+'
                if (!((currentChar == '-' || currentChar == '+') && (nextChar == '-' || nextChar == '+')))
                    continue;

                // remove the current char
                content = content.Remove(i, 1);
                // remove the next char
                content = content.Remove(i, 1);

                if (currentChar == nextChar)
                    content = content.Insert(i, "+");
                else
                    content = content.Insert(i, "-");

                i--;
            }
            // Step 2 done
            // Step 3 & 4 - split before and after every connector (* & /), split before every sign, thats not at the beginning of the part

            List<string> equationParts = new List<string>();
            string nextPart = "";

            for (int i = 0; i < content.Length; i++)
            {
                char currentChar = content[i];
                
                // if a + or - is at the beginning, it is included in the next number, if it's not at the beginning, a number started
                if((currentChar == '+' || currentChar == '-') && nextPart.Length > 0)
                {
                    equationParts.Add(nextPart);
                    nextPart = currentChar.ToString();
                }
                // connectors (* & /) are standalone parts
                else if(currentChar == '*' || currentChar == '/')
                {
                    equationParts.Add(nextPart);
                    nextPart = "";
                    equationParts.Add(currentChar.ToString());
                }
                else
                    nextPart += currentChar;
            }
            equationParts.Add(nextPart);

            // Step 3 + 4 end
            // multiplie and divide
            // the form of the equation is now e.g.:
            // | 4 | +3 | -5 | * | -6 | / | 3|

            for(int i = 0; i < equationParts.Count; i++)
            {
                string? resultString = null;
                if (equationParts[i].Equals("*"))
                {
                    resultString = MultiplieString(equationParts[i-1], equationParts[i + 1]).ToString();
                }
                else if (equationParts[i].Equals("/"))
                {
                    try
                    {
                        resultString = DivideString(equationParts[i - 1], equationParts[i + 1]).ToString();
                    }
                    catch (DivideByZeroException)
                    {
                        return false;
                    }
                }
                if(resultString != null)
                {
                    equationParts.RemoveAt(i + 1);
                    equationParts[i] = resultString;
                    equationParts.RemoveAt(i - 1);
                    i--;
                }
            }

            // add everything together
            // the equation only consists of numbers now

            double result = 0f;
            if (equationParts.Count > 0)
            {
                foreach(string equationPart in equationParts)
                {
                    result += double.Parse(equationPart);
                }
            }

            solution = result;

            return true;
        }

        public double GetSolution()
        {
            return solution;
        }

        [Obsolete]
        public string GetSolutionString()
        {
            return content;
        }

        double MultiplieString(string x, string y)
        {
            double doubleX = double.Parse(x);
            double doubleY = double.Parse(y);

            return doubleX * doubleY;
        }

        double DivideString(string x, string y)
        {
            double doubleX = double.Parse(x);
            double doubleY = double.Parse(y);

            if (doubleY == 0)
                throw new DivideByZeroException();

            return doubleX / doubleY;
        }
    }
}
