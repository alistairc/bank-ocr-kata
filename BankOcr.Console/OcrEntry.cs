namespace BankOcr.Console;

public record OcrEntry(OcrChar[] Characters)
{
    const int DigitsInEntry = 9;

    public static OcrEntry ParseCharacters(string input)
    {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        var rawDigits = Enumerable.Range(0, DigitsInEntry)
            .Select(digitNo => SelectTextForDigit(lines, digitNo))
            .TakeWhile(digitText => !string.IsNullOrEmpty(digitText));

        var parsedDigits = rawDigits
            .Select(text => OcrChar.TryParse(text))
            .Where(parsed => parsed != null);

        return new OcrEntry(parsedDigits.ToArray()!);
    }

    public string FormatLine()
    {
        return new string(Characters.Select(c => c.Character).ToArray());
    }

    static string SelectCharsForDigit(string line, int digitNo)
    {
        var startIndex = digitNo * OcrChar.CharacterWidth;
        return startIndex < line.Length ?
            line.Substring(startIndex, OcrChar.CharacterWidth)
            : string.Empty;
    }

    static string SelectTextForDigit(string[] lines, int digitNo)
    {
        var digitLines = lines.Select(line => SelectCharsForDigit(line, digitNo));
        return string.Join('\n', digitLines);
    }
}