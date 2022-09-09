using BankOcr.Console;

namespace BankOcr.Tests;

class FormattingOutput
{
    [Test]
    public void ShouldOutputOneEntryPerLine()
    {
        var entries = new[]
        {
            OcrEntry.FromAccountNumber("123456789"), 
            OcrEntry.FromAccountNumber("987654321")
        };

        var output = AccountNumberReport.ForEntries(entries).ToArray();

        output.Length.ShouldBe(2);
        output[0].ShouldStartWith("123456789");
        output[1].ShouldStartWith("987654321");
    }

    [Test]
    public void ShouldValidateTheAccountNumbers()
    {
        var entries = new[]
        {
            OcrEntry.FromAccountNumber("123"), 
            OcrEntry.FromAccountNumber("000000000")
        };

        var output = AccountNumberReport.ForEntries(entries).ToArray();

        output.Length.ShouldBe(2);
        output[0].ShouldEndWith(" ERR");
        output[1].ShouldNotEndWith(" ERR");
    }

    [Test]
    public void ShouldReportIllegibleNumbers()
    {
        var entries = new[]
        {
            OcrEntry.FromAccountNumber("123?567?9"), 
            OcrEntry.FromAccountNumber("000000000")
        };

        var output = AccountNumberReport.ForEntries(entries).ToArray();

        output.Length.ShouldBe(2);
        output[0].ShouldEndWith(" ILL");
        output[1].ShouldNotEndWith(" ILL");
    }
}