using BankOcr.Console;

namespace BankOcr.Tests;

class FormattingOutput
{
    [Test]
    public void ShouldOutputOneEntryPerLine()
    {
        var entries = new[]
        {
            OcrEntry.FromString("123456789"), 
            OcrEntry.FromString("987654321")
        };

        var output = AccountNumberReport.ForEntries(entries).ToArray();

        output.Length.ShouldBe(2);
        output[0].ShouldBe("123456789");
        output[1].ShouldBe("987654321");
    }
}