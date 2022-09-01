using BankOcr.Console;

namespace BankOcr.Tests;

class ParsingDigits
{
    [Test]
    public void ShouldParse1()
    {
        string[] digitLines = {
            "   ",
            "  |",
            "  |"
        };

        var parsed = OcrChar.TryParse(digitLines);

        parsed.ShouldBe(new OcrChar('1'));
    }

    [Test]
    public void ShouldHandleParsingFailures()
    {
        string[] digitLines = {
            "!!!",
            "!!!",
            "!!!"
        };

        var parsed = OcrChar.TryParse(digitLines);

        parsed.ShouldBeNull();
    }
}
