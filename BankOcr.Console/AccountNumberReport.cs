namespace BankOcr.Console;

public static class AccountNumberReport
{
    public static IEnumerable<string> ForEntries(IEnumerable<OcrEntry> entries)
    {
        return entries.Select(FormatEntry);
    }

    static string FormatEntry(OcrEntry entry)
    {
        var validationSuffix = GetValidationSuffix(entry);
        return $"{entry.AccountNumber}{validationSuffix}";
    }

    static string GetValidationSuffix(OcrEntry entry)
    {
        if (entry.AccountNumber.Contains('?'))
        {
            return " ILL";
        }

        if (entry.ValidationStatus == EntryValidationStatus.Invalid)
        {
            return " ERR";
        }

        return string.Empty;
    }
}