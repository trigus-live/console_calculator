using Calculator.Command;
using Calculator.Main;
using Calculator.Util;

internal class Program
{
    // maybe clean up in the future (a CommandExecutor should be able to access this)
    public static bool requestExit { get; set; } = false;

    private static void Main(string[] args)
    {
        // init everything needed
        CommandInterpreter commandInterpreter = CommandInterpreter.Instance;
        CalculatorEnginge calculatorEngine = new CalculatorEnginge();


        //welcome line
        Console.WriteLine("Welcome to a basic console-based calculator. Use '/help' for help.");


        // will be set to 'true' if the user want to exit the programm
        bool exitApp = false;


        /*
         * step 0: print leading String
         * step 1: get next prompt
         * step 2: check, if prompt is empty
         *              if true -> skip to next loop
         * step 3: check, if prompt is command
         *              if true -> assign prompt content to commandInterpreter
         *              if false -> assign prompt content to calculatorEngine
         * step 4: check, if the program should be exited
         */
        while (!exitApp)
        {
            Console.Write("> ");

            Prompt nextPrompt = Prompt.GetNextPrompt();

            if (nextPrompt == null)
                continue;

            if (nextPrompt.IsCommand())
            {
                commandInterpreter.ParseCommand(nextPrompt.Content);
            }
            else
            {

                Equation? equation = calculatorEngine.PrepareEquation(nextPrompt.Content);
                if (equation != null)
                {
                    bool success = equation.Solve();
                    if (success)
                        Console.WriteLine(equation.GetSolution());
                    else
                        Console.WriteLine("Error while solving - check for \"divide by zero\" occurence");
                }
                else
                    Console.WriteLine("invalid equation entered");
            }

            // step 4
            exitApp = requestExit;
        }

        Console.WriteLine("\nHave a beautiful day!");
    }
}