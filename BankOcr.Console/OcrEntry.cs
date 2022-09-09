namespace BankOcr.Console;

public record OcrEntry
{
    static readonly string Padding = new(' ', OcrChar.CharacterWidth);

    // Convenience factory method, mainly for tests
    public static OcrEntry FromAccountNumber(string entryText)
    {
        var characters = entryText
            .Select(chr => chr == '?' ? null : new OcrChar(chr))
            .ToArray();
        return Validate(characters);
    }

    public static OcrEntry ParseCharacters(TextRectangle input)
    {
        var lines = input.MultiLineText.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        var rawDigits = Enumerable.Range(0, int.MaxValue)
            .Select(digitNo => SelectTextForDigit(lines, digitNo))
            .TakeWhile(digitText => !string.IsNullOrWhiteSpace(digitText));

        var parsedDigits = rawDigits.Select(selection => OcrChar.TryParse(new TextRectangle(selection)));

        return Validate(parsedDigits.ToArray());
    }

    public bool IsValidAccountNumber { get; }
    public string AccountNumber { get; } 

    OcrEntry(bool isValid, string accountNumber)
    {
        IsValidAccountNumber = isValid;
        AccountNumber = accountNumber;
    }

    static OcrEntry Validate(OcrChar?[] characters)
    {
        var parsedCharacters = characters.Where(c => c != null).ToArray();
        var isValid = parsedCharacters.Length == 9 && ChecksumIsValid(parsedCharacters!);
        var accountNumber = new string(characters.Select(c => c?.Character ?? '?').ToArray());
        return new OcrEntry(isValid, accountNumber);
    }

    static bool ChecksumIsValid(OcrChar[] parsedCharacters)
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
    
    static string SelectCharsForDigit(string line, int digitNo)
    {
        // it simplifies things if there's always some trailing space
        var paddedLine = line + Padding;

        var startIndex = digitNo * OcrChar.CharacterWidth;
        if (startIndex < line.Length)
        {
            return paddedLine.Substring(startIndex, OcrChar.CharacterWidth);
        }

        return string.Empty;
    }

    static string SelectTextForDigit(string[] lines, int digitNo)
    {
        var digitLines = lines.Select(line => SelectCharsForDigit(line, digitNo));
        return string.Join('\n', digitLines);
    }
}