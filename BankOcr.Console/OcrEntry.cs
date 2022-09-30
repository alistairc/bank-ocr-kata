namespace BankOcr.Console;

public record OcrEntry
{
    const int RequiredDigits = 9;

    OcrEntry(EntryValidationStatus validationStatus, string accountNumber)
    {
        ValidationStatus = validationStatus;
        AccountNumber = accountNumber;
    }

    public EntryValidationStatus ValidationStatus { get; }
    public string AccountNumber { get; }

    // Convenience factory method, mainly for tests
    public static OcrEntry FromAccountNumber(string entryText)
    {
        var digits = entryText
            .Select(chr => chr == '?' ? null : new OcrDigit(chr))
            .ToArray();
        return Validate(digits);
    }

    public static OcrEntry Parse(TextRectangle input)
    {
        var rawDigits = Enumerable.Range(0, int.MaxValue)
            .Select(digitNo => SelectDigit(input, digitNo))
            .TakeWhile(digitText => !digitText.IsBlank);

        var parsedDigits = rawDigits.Select(OcrDigit.TryParse);

        return Validate(parsedDigits.ToArray());
    }

    static OcrEntry Validate(OcrDigit?[] digits)
    {
        var validationStatus = GetStatus(digits);
        var accountNumber = new string(digits.Select(d => d?.Character ?? '?').ToArray());
        return new OcrEntry(validationStatus, accountNumber);
    }

    static EntryValidationStatus GetStatus(OcrDigit?[] digits)
    {
        OcrDigit[] parsedDigits = digits
            .Where(c => c != null)
            .ToArray()!;

        if (parsedDigits.Length < digits.Length)
        {
            return EntryValidationStatus.Illegible;
        }

        if (ChecksumIsValid(parsedDigits))
        {
            return EntryValidationStatus.Ok;
        }

        return EntryValidationStatus.Invalid;
    }


    static bool ChecksumIsValid(IReadOnlyCollection<OcrDigit> parsedDigits)
    {
        if (parsedDigits.Count != RequiredDigits) { return false; }
        var digits = parsedDigits.Select(c => c.Digit).ToArray();

        var sum = digits
            .Select((digit, index) => digit * (RequiredDigits - index))
            .Sum();

        var checksum = sum % 11;
        return checksum == 0;
    }

    static TextRectangle SelectDigit(TextRectangle source, int digitNo)
    {
        var startIndex = digitNo * OcrDigit.CharacterWidth;
        return source.Select(startIndex, 0, OcrDigit.CharacterWidth, OcrDigit.CharacterHeight);
    }
}