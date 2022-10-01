using BankOcr.Console;

namespace BankOcr.Tests;

public class RunningTheProgram
{
    const string Input =
        "    _  _     _  _  _  _  _ \n" +
        "  | _| _||_||_ |_   ||_||_|\n" +
        "  ||_  _|  | _||_|  ||_| _|\n";

    [Test]
    public void ShouldWriteToTheConsole()
    {
        var sut = new InMemoryProgramSystem();
        sut.RunProgramWithValidInput();
        sut.GetConsoleText().ShouldBe("123456789" + Environment.NewLine);
    }

    [Test]
    public void ShouldWriteToTheOutputFile()
    {
        var sut = new InMemoryProgramSystem();
        sut.RunProgramWithValidInput();
        sut.GetOutputFileText().ShouldBe("123456789" + Environment.NewLine);
    }

    [Test]
    public void OutputFileShouldBeOptional()
    {
        var sut = new InMemoryProgramSystem();
        sut.RunProgramWithNoOutputFile();
        sut.GetOutputFileText().ShouldBeNull();
    }

    class InMemoryProgramSystem
    {
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
}