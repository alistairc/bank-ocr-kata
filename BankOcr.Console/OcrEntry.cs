namespace BankOcr.Console;

public record OcrEntry
{
    // Convenience factory method, mainly for tests
    public static OcrEntry FromAccountNumber(string entryText)
    {
        var characters = entryText
            .Select(chr => chr == '?' ? null : new OcrDigit(chr))
            .ToArray();
        return Validate(characters);
    }

    public static OcrEntry ParseCharacters(TextRectangle input)
    {
        var rawDigits = Enumerable.Range(0, int.MaxValue)
            .Select(digitNo => SelectDigit(input, digitNo))
            .TakeWhile(digitText => !digitText.IsBlank);

        var parsedDigits = rawDigits.Select(OcrDigit.TryParse);

        return Validate(parsedDigits.ToArray());
    }

    public bool IsValidAccountNumber { get; }
    public string AccountNumber { get; } 

    OcrEntry(bool isValid, string accountNumber)
    {
        IsValidAccountNumber = isValid;
        AccountNumber = accountNumber;
    }

    static OcrEntry Validate(OcrDigit?[] characters)
    {
        var parsedCharacters = characters.Where(c => c != null).ToArray();
        var isValid = parsedCharacters.Length == 9 && ChecksumIsValid(parsedCharacters!);
        var accountNumber = new string(characters.Select(c => c?.Character ?? '?').ToArray());
        return new OcrEntry(isValid, accountNumber);
    }

    static bool ChecksumIsValid(IEnumerable<OcrDigit> parsedCharacters)
    {
        var digits = parsedCharacters.Select(c => c.Digit).ToArray();

        var sum =
            (digits[8] * 1) +
            (digits[7] * 2) +
            (digits[6] * 3) +
            (digits[5] * 4) +
            (digits[4] * 5) +
            (digits[3] * 6) +
            (digits[2] * 7) +
            (digits[1] * 8) +
            (digits[0] * 9);

        var checksum = sum % 11;
        return checksum == 0;
    }
    
    static TextRectangle SelectDigit(TextRectangle source, int digitNo)
    {
        var startIndex = digitNo * OcrDigit.CharacterWidth;
        return source.Select(startIndex, 0, OcrDigit.CharacterWidth, OcrDigit.CharacterHeight);
    }
}