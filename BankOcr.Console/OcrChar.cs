namespace BankOcr.Console;

public record OcrChar
{
    public const int CharacterWidth = 3;

    public OcrChar(char character)
    {
        if (!char.IsDigit(character))
        {
            throw new ArgumentOutOfRangeException(nameof(character), "Must be a digit");
        }

        Character = character;
    }

    public char Character { get; init; }
    public int Digit => int.Parse(Character.ToString());

    public static OcrChar? TryParse(string digitText)
    {
        var found = KnownDigits.TryGetValue(digitText, out var character);

        return found ? new OcrChar(character) : null;
    }

    static readonly Dictionary<string, char> KnownDigits = new() {
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
            "|_ "
            , '2'
        },
        {
            " _ \n" +
            " _|\n" +
            " _|"
            , '3'
        },
        {
            "   \n" +
            "|_|\n" +
            "  |"
            , '4'
        },
        {
            " _ \n" +
            "|_ \n" +
            " _|"
            , '5'
        },
        {
            " _ \n" +
            "|_ \n" +
            "|_|"
            , '6'
        },
        {
            " _ \n" +
            "  |\n" +
            "  |"
            , '7'
        },
        {
            " _ \n" +
            "|_|\n" +
            "|_|"
            , '8'
        },
        {
            " _ \n" +
            "|_|\n" +
            " _|"
            , '9'
        }
    };
}