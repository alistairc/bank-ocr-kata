using BankOcr.Console;

namespace BankOcr.Tests;

public class ParsingCommandLineArgs
{
    [Test]
    public void TwoArgs_ShouldBeInputAndOutputFiles()
    {
        var parsed = ProgramOptions.ParseArgs(new[] { "first", "second" });

        parsed.IsValid.ShouldBeTrue();
        parsed.SourcePath.ShouldBe("first");
        parsed.OutputPath.ShouldBe("second");
    }

    [Test]
    public void OneArg_ShouldBeInputFileOnly()
    {
        var parsed = ProgramOptions.ParseArgs(new[] { "first" });

        parsed.IsValid.ShouldBeTrue();
        parsed.SourcePath.ShouldBe("first");
        parsed.OutputPath.ShouldBeNull();
    }

    [Test]
    public void NoArgs_ShouldBeInvalid()
    {
        var parsed = ProgramOptions.ParseArgs(new string[0]);

        parsed.IsValid.ShouldBeFalse();
        parsed.SourcePath.ShouldBeNull();
        parsed.OutputPath.ShouldBeNull();
    }
}