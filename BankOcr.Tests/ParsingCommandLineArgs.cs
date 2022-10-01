using BankOcr.Console;

namespace BankOcr.Tests;

public class ParsingCommandLineArgs
{
    [Test]
    public void TwoArgs_ShouldBeInputAndOutputFiles()
    {
        var parsed = ProgramOptions.ParseArgs(new[] { "first", "second" });
        
        parsed.SourcePath.ShouldBe("first");
        parsed.OutputPath.ShouldBe("second");
    }
    
    [Test]
    public void OneArg_ShouldBeInputFileOnly()
    {
        var parsed = ProgramOptions.ParseArgs(new[] { "first" });
        
        parsed.SourcePath.ShouldBe("first");
        parsed.OutputPath.ShouldBeNull();
    }
}