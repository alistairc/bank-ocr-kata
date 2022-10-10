namespace BankOcr.Tests.RunningTheProgram;

public class WithValidArgs
{
    [Test]
    public void ShouldWriteToTheConsole()
    {
        var sut = new InMemoryProgramSystem();
        sut.RunProgramWithValidInput();
        sut.GetConsoleText().ShouldBe(InMemoryProgramSystem.ExpectedOutput);
    }

    [Test]
    public void ShouldWriteToTheOutputFile()
    {
        var sut = new InMemoryProgramSystem();
        sut.RunProgramWithValidInput();
        sut.GetOutputFileText().ShouldBe(InMemoryProgramSystem.ExpectedOutput);
    }

    [Test]
    public void OutputFileShouldBeOptional()
    {
        var sut = new InMemoryProgramSystem();
        sut.RunProgramWithNoOutputFile();
        sut.GetOutputFileText().ShouldBeNull();
    }
}