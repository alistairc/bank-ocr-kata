using BankOcr.Console;

namespace BankOcr.Tests.RunningTheProgram;

public class WithInvalidArgs
{
    [Test]
    public void ShouldWriteUsageMessage()
    {
        var sut = new InMemoryProgramSystem();
        sut.RunProgramWithNoArgs();
        sut.GetConsoleText().ShouldBe(BankOcrProgram.UsageMessage + Environment.NewLine);
    }

    [Test]
    public void ShouldExitWithInvalidArgsCode()
    {
        var sut = new InMemoryProgramSystem();
        sut.RunProgramWithNoArgs();
        sut.ExitCode.ShouldBe(ExitCode.InvalidArgs);
    }
}