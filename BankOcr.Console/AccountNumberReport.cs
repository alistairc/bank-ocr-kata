namespace BankOcr.Console;

public static class AccountNumberReport
{
    public static IEnumerable<string> ForEntries(IEnumerable<OcrEntry> entries)
    {
        return entries.Select(entry => entry.AccountNumber);
    }
}