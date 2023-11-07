using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Main
{
    internal class CalculatorEnginge
    {

        /*
         * Maybe at some point all numbers 0-9 should be contained in an array, so that this array can be reused in
         * this dictionary, so that no numbers have to be added a thousand times
         */
        readonly Dictionary<char, char[]> characterFollowMap = new Dictionary<char, char[]>(){
            { '0', new char[]{'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', '+', '-', '*', '/', ')'} },
            { '1', new char[]{'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', '+', '-', '*', '/', ')'} },
            { '2', new char[]{'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', '+', '-', '*', '/', ')'} },
            { '3', new char[]{'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', '+', '-', '*', '/', ')'} },
            { '4', new char[]{'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', '+', '-', '*', '/', ')'} },
            { '5', new char[]{'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', '+', '-', '*', '/', ')'} },
            { '6', new char[]{'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', '+', '-', '*', '/', ')'} },
            { '7', new char[]{'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', '+', '-', '*', '/', ')'} },
            { '8', new char[]{'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', '+', '-', '*', '/', ')'} },
            { '9', new char[]{'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ',', '+', '-', '*', '/', ')'} },
            { '+', new char[]{'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '-', '('} },
            { '-', new char[]{'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '-', '('} },
            { '*', new char[]{'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '-', '('} },
            { '/', new char[]{'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '-', '('} },
            { '(', new char[]{'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '-', '(', ')'} },
            { ')', new char[]{'+', '-', '*', '/', ')'} },
            { ',', new char[]{ '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' } }
        };

        public CalculatorEnginge() { }

        /*
         * Checks the input and returns an equation instance, thats ready to solve, if the input is valid
         * returns null otherwise
         * Prepare Equation
         * 
         * 1. Remove every space
         * 2. Turn every '.' into ','
         * 3. Add '(' at the beginning and ')' at the end 
         * 4. Add a '*' between every number and an opening bracket
         * 5. Check: only allowed characters are: [0-9][+-/*(),.]
         * 6. Check: there must be an equal amount of '(' and ')'
         * 7. Check for the accepted form (see EquationHasAcceptedForm())
         */
        public Equation? PrepareEquation(string input)
        {
            if (input == null) return null;
            if (input.Length == 0) return null;

            input = input.Replace(" ", "");
            input = input.Replace(".", ",");

            input = input.Insert(0, "(");
            input = input.Insert(input.Length, ")");

            input = AddTimesSignBeforeOpeningBracket(input);

            if (!EquationOnlyIncludeAllowedCharacters(input)) return null;
            if (!EquationHasEqualAmountOfOpenAndCloseBrackets(input)) return null;
            if (!EquationHasAcceptedForm(input)) return null;

            return new Equation(input);
        }

        string AddTimesSignBeforeOpeningBracket(string input)
        {
            char[] numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            for (int i = 0; i < input.Length; i++)
            {
                // if this check is true, we got the last char
                if (input.Length == i + 1) continue;

                char currentChar = input[i];
                char nextChar = input[i + 1];
                
                if (numbers.Contains(currentChar) && nextChar == '(')
                {
                    input = input.Insert(i + 1, "*");
                }
            }

            return input;
        }

        public bool EquationOnlyIncludeAllowedCharacters(string input)
        {
            if (input == null) return false;
            char[] allowedCharacters = characterFollowMap.Keys.ToArray();

            foreach (char c in input)
                if (!allowedCharacters.Contains(c))
                    return false;
            return true;
        }

        public bool EquationHasEqualAmountOfOpenAndCloseBrackets(string input)
        {
            if (input == null) return false;

            int openBracketCount = 0;
            int closeBracketCount = 0;

            foreach (char c in input)
            {
                if (c == '(')
                    openBracketCount++;
                else if (c == ')')
                    closeBracketCount++;
            }

            return openBracketCount == closeBracketCount;
        }

        /*
         *  
         * Accepted form:
         * 1. equation has to start with '('
         * 2. equation has to end with ')'
         * 3. every char may only be followed by a character defined in the characterFollowMap
         */
        public bool EquationHasAcceptedForm(string input)
        {
            if (input == null) return false;
            if (input.Length == 0) return false;

            if (input[0] != '(') return false;
            if (input[input.Length - 1] != ')') return false;

            for (int i = 0; i < input.Length; i++)
            {
                // if this check is true, we got the last char, which has already been checked
                if (input.Length == i + 1) continue;

                char currentChar = input[i];
                char nextChar = input[i + 1];

                if (!characterFollowMap[currentChar].Contains(nextChar))
                {
                    return false;
                }

            }

            return true;
        }
    }
}
