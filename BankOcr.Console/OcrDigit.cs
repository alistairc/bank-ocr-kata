namespace BankOcr.Console;

public record OcrDigit
{
    public const int CharacterWidth = 3;
    public const int CharacterHeight = 3;

    static readonly Dictionary<string, char> KnownDigits = new()
    {
        {
            " _ \n" +
            "| |\n" +
            "|_|",
            '0'
        },
        {
            "   \n" +
            "  |\n" +
            "  |",
            '1'
        },
        {
            " _ \n" +
            " _|\n" +
            "|_ ",
            '2'
        },
        {
            " _ \n" +
            " _|\n" +
            " _|",
            '3'
        },
        {
            "   \n" +
            "|_|\n" +
            "  |",
            '4'
        },
        {
            " _ \n" +
            "|_ \n" +
            " _|",
            '5'
        },
        {
            " _ \n" +
            "|_ \n" +
            "|_|",
            '6'
        },
        {
            " _ \n" +
            "  |\n" +
            "  |",
            '7'
        },
        {
            " _ \n" +
            "|_|\n" +
            "|_|",
            '8'
        },
        {
            " _ \n" +
            "|_|\n" +
            " _|",
            '9'
        }
    };

    public OcrDigit(char character)
    {
        if (!char.IsDigit(character))
        {
            throw new ArgumentOutOfRangeException(nameof(character), "Must be a digit");
        }

        Character = character;
        Digit = int.Parse(Character.ToString());
    }

    public char Character { get; }
    public int Digit { get; }

    public static OcrDigit? TryParse(TextRectangle digitText)
    {
        var found = KnownDigits.TryGetValue(digitText.MultiLineText, out var character);

        return found ? new OcrDigit(character) : null;
    }
}