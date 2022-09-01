namespace BankOcr.Console;

public record OcrChar(char Character)
{
    static readonly string KnownDigit =
            "   \n" +
            "  |\n" +
            "  |";

    public static OcrChar? TryParse(string[] digitLines)
    {
        if (string.Join('\n', digitLines) == KnownDigit)
        {
            return new OcrChar('1');
        }
        return null;
    }
}