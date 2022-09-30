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
        var parsed = OcrDigit.TryParse(TextRectangle.FromString(source));

        parsed.ShouldBe(new OcrDigit(expected));
    }

    [Test]
    public void ShouldHandleParsingFailures()
    {
        var digitLines =
            "!!!\n" +
            "!!!\n" +
            "!!!";

        var parsed = OcrDigit.TryParse(TextRectangle.FromString(digitLines));

        parsed.ShouldBeNull();
    }
}