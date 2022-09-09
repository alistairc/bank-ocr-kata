namespace BankOcr.Console;

public record OcrEntry(OcrChar?[] Characters)
{
    static readonly string Padding = new(' ', OcrChar.CharacterWidth);

    // Convenience factory method, mainly for tests
    public static OcrEntry FromString(string entryText)
    {
        var characters = entryText.Select(chr => new OcrChar(chr)).ToArray();
        return new OcrEntry(characters);
    }

    public static OcrEntry ParseCharacters(string input)
    {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        var rawDigits = Enumerable.Range(0, int.MaxValue)
            .Select(digitNo => SelectTextForDigit(lines, digitNo))
            .TakeWhile(digitText => !string.IsNullOrWhiteSpace(digitText));

        var parsedDigits = rawDigits.Select(OcrChar.TryParse);

        return new OcrEntry(parsedDigits.ToArray());
    }

    public string AccountNumber => new string(Characters.Select(c => c?.Character ?? '?').ToArray());
    public OcrChar[] ParsedCharacters => Characters.Where(c => c != null).ToArray()!;

    public bool ValidateAccountNumber()
    {
        return ParsedCharacters.Length == 9 && ChecksumIsValid();
    }

    bool ChecksumIsValid()
    {
        var digits = ParsedCharacters.Select(c => c.Digit).ToArray();

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