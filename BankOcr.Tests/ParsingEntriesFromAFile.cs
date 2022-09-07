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
        var outputWriter = new StringWriter();
        OcrEntryFile.ProcessInputText(lines, outputWriter);

        var outputLines = outputWriter.ToString().Split(Environment.NewLine);
        outputLines[0].ShouldBe("111111111");
        outputLines[2].ShouldBe("333333333");
    }
}