using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Command
{
    internal class CommandInfo : CommandExecutor
    {
        public CommandInfo(string label, string description) : base(label, description)
        {

        }

        public override void Execute()
        {
            Console.WriteLine("Information on how to use the calculator:\n");
            Console.WriteLine("Enter you term an press [ENTER]. Usable symbols are:");
            Console.WriteLine("0-9 + - * / , . ( ) \n");
            Console.WriteLine("Your term must be a valid mathematical expression.\n");
            Console.WriteLine("Additional Info:");
            Console.WriteLine("Consecutive '+' and/or '-' are allowed.");
            Console.WriteLine("A number, followed by '(' without a '*' is allowed.");
            Console.WriteLine("Interchangable use of '.' and ',' is allowed.");
        }
    }
}
