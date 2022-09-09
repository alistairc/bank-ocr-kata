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
        return entry.ValidationStatus switch
        {
            EntryValidationStatus.Ok => string.Empty,
            EntryValidationStatus.Illegible => " ILL",
            EntryValidationStatus.Invalid => " ERR",
            _ => throw new ArgumentOutOfRangeException(nameof(entry), entry.ValidationStatus.ToString())
        };
    }
}