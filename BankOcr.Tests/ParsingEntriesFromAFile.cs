using BankOcr.Console;

namespace BankOcr.Tests;

class ParsingEntriesFromAFile
{
    [Test]
    public void ShouldParseAndPrintMultipleEntries()
    {
        string[] lines =
        {
            "                           ",
            "  |  |  |  |  |  |  |  |  |",
            "  |  |  |  |  |  |  |  |  |",
            "",
            " _  _  _  _  _  _  _  _  _ ",
            " _| _| _| _| _| _| _| _| _|",
            "|_ |_ |_ |_ |_ |_ |_ |_ |_ ",
            "",
            " _  _  _  _  _  _  _  _  _ ",
            " _| _| _| _| _| _| _| _| _|",
            " _| _| _| _| _| _| _| _| _|",
            ""
        };
        var entries = OcrEntryFile.ParseEntries(lines).ToArray();

        entries[0].AccountNumber.ShouldBe("111111111");
        entries[2].AccountNumber.ShouldBe("333333333");
    }
}