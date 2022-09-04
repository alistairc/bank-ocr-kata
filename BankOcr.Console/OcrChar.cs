namespace BankOcr.Console;

public record OcrChar(char Character)
{
    public const int CharacterWidth = 3;

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

    public static OcrChar? TryParse(string digitText)
    {
        var found = KnownDigits.TryGetValue(digitText, out var character);

        return found ? new OcrChar(character) : null;
    }
}