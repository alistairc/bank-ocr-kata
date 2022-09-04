namespace BankOcr.Console;

public record OcrEntry(OcrChar[] Characters)
{
    const int DigitsInEntry = 9;

    public static OcrEntry ParseCharacters(string input)
    {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        var rawDigits = Enumerable.Range(0, DigitsInEntry)
            .Select(digitNo => SelectTextForDigit(lines, digitNo));

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
        return line.Substring(digitNo * OcrChar.CharacterWidth, OcrChar.CharacterWidth);
    }

    static string SelectTextForDigit(string[] lines, int digitNo)
    {
        var digitLines = lines.Select(line => SelectCharsForDigit(line, digitNo));
        return string.Join('\n', digitLines);
    }
}