namespace BankOcr.Console;

public record TextRectangle
{
    public static readonly TextRectangle Empty = new TextRectangle(string.Empty, 0, 0);

    TextRectangle(string multiLineText, int width, int height)
    {
        MultiLineText = multiLineText;
        Width = width;
        Height = height;
    }

    public static TextRectangle FromString(string multiLineText)
    {
        if (string.IsNullOrEmpty(multiLineText))
        {
            return TextRectangle.Empty;
        }

        var normalized = multiLineText.Replace("\r\n", "\n").Replace("\r", "\n");
        var lines = normalized.Split('\n');
        var width = lines.Max(line => line.Length);
        var height = lines.Length;
        var padded = lines.Select(line => line.PadRight(width)).ToArray();
        var multiline = string.Join('\n', padded);
        return new TextRectangle(multiline, width, height);
    }

    public string MultiLineText { get; }
    public int Width { get; }
    public int Height  { get; }
}