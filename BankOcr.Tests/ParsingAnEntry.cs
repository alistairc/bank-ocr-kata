using BankOcr.Console;

namespace BankOcr.Tests;

class ParsingAnEntry
{
    [Test]
    public void ShouldParseAFullEntry()
    {
        var input =
            "    _  _     _  _  _  _  _ \n" +
            "  | _| _||_||_ |_   ||_||_|\n" +
            "  ||_  _|  | _||_|  ||_| _|\n";

        var entry = OcrEntry.ParseCharacters(input);

        entry.FormatLine().ShouldBe("123456789");
    }

    [Test]
    public void ShouldParseASingleDigit()
    {
        var input =
            "   \n" +
            "  |\n" +
            "  |\n";

        var entry = OcrEntry.ParseCharacters(input);

        entry.FormatLine().ShouldBe("1");
    }
}