namespace BankOcr.Console;

public static class AccountNumberReport
{
    public static IEnumerable<string> ForEntries(IEnumerable<OcrEntry> entries)
    {
        return entries.Select(FormatEntry);
    }

    static string FormatEntry(OcrEntry entry)
    {
        var validationSuffix = entry.IsValidAccountNumber ? "" : " ERR";
        return $"{entry.AccountNumber}{validationSuffix}";
    }
}