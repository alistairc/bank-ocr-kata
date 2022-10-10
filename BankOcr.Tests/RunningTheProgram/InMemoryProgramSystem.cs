using BankOcr.Console;

namespace BankOcr.Tests.RunningTheProgram;

class InMemoryProgramSystem
    {
        const string Input =
            "    _  _     _  _  _  _  _ \n" +
            "  | _| _||_||_ |_   ||_||_|\n" +
            "  ||_  _|  | _||_|  ||_| _|\n";

        public static readonly string ExpectedOutput = "123456789" + Environment.NewLine;

        StringWriter StdOut { get; } = new();
        InMemoryStreamFinder StreamFinder { get; } = new();

        public void RunProgramWithValidInput()
        {
            StreamFinder.SetupFile("input.txt", Input);

            var program = new BankOcrProgram(StreamFinder, StdOut);
            program.Run(new ProgramOptions("input.txt", "output.txt"));
        }

        public void RunProgramWithNoOutputFile()
        {
            StreamFinder.SetupFile("input.txt", Input);

            var program = new BankOcrProgram(StreamFinder, StdOut);
            program.Run(new ProgramOptions("input.txt", null));
        }

        public string GetConsoleText()
        {
            return StdOut.ToString();
        }

        public string? GetOutputFileText()
        {
            return StreamFinder.GetFile("output.txt");
        }
    }
