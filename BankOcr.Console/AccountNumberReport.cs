namespace BankOcr.Console;

public class AccountNumberReport
{
    public AccountNumberReport(IEnumerable<OcrEntry> entries)
    {
        Entries = entries;
    }

    IEnumerable<OcrEntry> Entries { get; }

    public void WriteTo(TextWriter writer)
    {
        var lines = Entries.Select(FormatEntry);
        foreach (var line in lines)
        {
            writer.WriteLine(line);
        }
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