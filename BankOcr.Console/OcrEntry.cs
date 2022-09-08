namespace BankOcr.Console;

public record OcrEntry(OcrChar[] Characters)
{
    static readonly string Padding = new(' ', OcrChar.CharacterWidth);

    public static OcrEntry ParseCharacters(string input)
    {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        var rawDigits = Enumerable.Range(0, int.MaxValue)
            .Select(digitNo => SelectTextForDigit(lines, digitNo))
            .TakeWhile(digitText => !string.IsNullOrWhiteSpace(digitText));

        var parsedDigits = rawDigits
            .Select(text => OcrChar.TryParse(text))
            .Where(parsed => parsed != null);

        return new OcrEntry(parsedDigits.ToArray()!);
    }

    public string AccountNumber => new string(Characters.Select(c => c.Character).ToArray());

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