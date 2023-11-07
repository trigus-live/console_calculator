using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Command
{
    internal abstract class CommandExecutor
    {

        public string label { get; }
        public string description { get; }

        public CommandExecutor(string label, string description)
        {
            this.label = label;
            this.description = description;
        }

        public abstract void Execute();
    }
}
