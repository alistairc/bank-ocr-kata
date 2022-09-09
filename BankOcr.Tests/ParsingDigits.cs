using BankOcr.Console;

namespace BankOcr.Tests;

class ParsingDigits
{
    [TestCase(
        " _ \n" +
        "| |\n" +
        "|_|",
        '0'
    )]
    [TestCase(
        "   \n" +
        "  |\n" +
        "  |"
        , '1'
    )]
    [TestCase(
        " _ \n" +
        "|_|\n" +
        " _|"
        , '9'
    )]
    public void ShouldParse(string source, char expected)
    {
        var parsed = OcrChar.TryParse(new TextRectangle(source));

        parsed.ShouldBe(new OcrChar(expected));
    }

    [Test]
    public void ShouldHandleParsingFailures()
    {
        string digitLines =
            "!!!\n" +
            "!!!\n" +
            "!!!";

        var parsed = OcrChar.TryParse(new TextRectangle(digitLines));

        parsed.ShouldBeNull();
    }
}
