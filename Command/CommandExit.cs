using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Command
{
    internal class CommandExit : CommandExecutor
    {
        public CommandExit(string label, string description) : base(label, description)
        { 
        
        }

        public override void Execute()
        {
            Program.requestExit = true;
        }
    }
}
