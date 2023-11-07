using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Util
{
    internal class Prompt
    {
        public const char COMMAND_INITIATOR = '/'; // the char, every command has to start with

        public string Content { get; } // the content of the prompt

        public Prompt(string content)
        {
            Content = content;
        }

        public bool IsCommand()
        {
            return Content.StartsWith(COMMAND_INITIATOR);
        }

        public bool IsEmpty()
        {
            return Content.Length == 0;
        }



        public static Prompt GetNextPrompt()
        {
            //Read the next line from the console and return a Prompt-Instance with the written line

            string input = Console.ReadLine();
            if (input == null)
                return new Prompt(null);
            return new Prompt(input);
        }
    }
}
