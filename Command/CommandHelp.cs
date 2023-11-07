using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Util;

namespace Calculator.Command
{
    internal class CommandHelp : CommandExecutor
    {
        public CommandHelp(string label, string description) : base(label, description)
        {
            
        }

        public override void Execute()
        {
            Console.WriteLine("Help:");
            foreach(CommandExecutor executor in CommandInterpreter.Instance.GetCommands())
            {
                // prints the help text for every command: e.g. /help: Prints a help text
                Console.WriteLine($"{Prompt.COMMAND_INITIATOR}{executor.label}: { executor.description}");
            }
        }
    }
}
