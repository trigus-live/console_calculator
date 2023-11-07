using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Util;

namespace Calculator.Command
{
    internal class CommandInterpreter
    {

        public static CommandInterpreter Instance { get; } = new CommandInterpreter();


        Dictionary<string, CommandExecutor> commands = new Dictionary<string, CommandExecutor>();

        public CommandInterpreter()
        {
            // add all commands
            commands.Add("help", new CommandHelp("help", "Prints a help text"));
            commands.Add("exit", new CommandExit("exit", "Exits the program"));
            commands.Add("info", new CommandInfo("info", "Prints information on how to use the calculator"));
        }

        public void ParseCommand(string command)
        {
            command = command.Remove(0, 1); // remove the trailing command-specific char

            if (commands.ContainsKey(command))
            {
                commands[command].Execute();
            }
            else
            {
                Console.WriteLine($"the command '{ command }' does not exist, please use '{Prompt.COMMAND_INITIATOR}help' for help.");
            }
        }

        public List<CommandExecutor> GetCommands()
        {
            return commands.Values.ToList();
        }
    }
}
