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

        entry.AccountNumber.ShouldBe("123456789");
    }

    [Test]
    public void ShouldNotLimitLengthOfEntries()
    {
        var input =
            "    _  _     _  _  _  _  _     _  _     _  _  _  _  _ \n" +
            "  | _| _||_||_ |_   ||_||_|  | _| _||_||_ |_   ||_||_|\n" +
            "  ||_  _|  | _||_|  ||_| _|  ||_  _|  | _||_|  ||_| _|\n";

        var entry = OcrEntry.ParseCharacters(input);

        entry.AccountNumber.ShouldBe("123456789123456789");
    }

    [Test]
    public void ShouldUseQuestionMarksForUnparsableDataInAccountNumber()
    {
        var input =
            "    _  _     _ \n" +
            "  ||_| _||_ |_ \n" +
            "  ||_  _|| | _|\n";

        var entry = OcrEntry.ParseCharacters(input);

        entry.AccountNumber.ShouldBe("1?3?5");
    }

    [Test]
    public void ShouldParseASingleDigit()
    {
        var input =
            "   \n" +
            "  |\n" +
            "  |\n";

        var entry = OcrEntry.ParseCharacters(input);

        entry.AccountNumber.ShouldBe("1");
    }

    [Test]
    public void ShouldTolerateBlankEntries()
    {
        var input =
            "\n" +
            "\n" +
            "\n";

        var entry = OcrEntry.ParseCharacters(input);

        entry.
        AccountNumber.ShouldBe(string.Empty);
    }

    [Test]
    public void ShouldTolerateMissingTrailingSpaces()
    {
        // first and third line are short
        var input =
            " _\n" +
            " _|\n" +
            "|_\n";

        var entry = OcrEntry.ParseCharacters(input);

        entry.AccountNumber.ShouldBe("2");
    }

    [Test]
    public void ShouldTolerateExtraTrailingSpaces()
    {
        var input =
            " _                     \n" +
            " _|\n" +
            "|_      \n";

        var entry = OcrEntry.ParseCharacters(input);

        entry.AccountNumber.ShouldBe("2");
    }

    [Test]
    public void ShouldTolerateExtraBlankLines()
    {
        var input =
            " _\n" +
            " _|\n" +
            "|_\n\n\n";

        var entry = OcrEntry.ParseCharacters(input);

        entry.AccountNumber.ShouldBe("2");
    }
}