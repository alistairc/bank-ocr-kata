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
        return FromLines(lines);
    }

    private static TextRectangle FromLines(string[] lines)
    {
        if (lines.Length==0)
        {
            return TextRectangle.Empty;
        }
        var width = lines.Max(line => line.Length);
        var height = lines.Length;
        var padded = lines.Select(line => line.PadRight(width)).ToArray();
        var multiline = string.Join('\n', padded);
        return new TextRectangle(multiline, width, height);
    }

    public TextRectangle Select(int x, int y, int width, int height)
    {
        if (x < 0) { throw new ArgumentOutOfRangeException(nameof(x), x.ToString());}
        if (y < 0) { throw new ArgumentOutOfRangeException(nameof(y), y.ToString());}
        if (width < 0) { throw new ArgumentOutOfRangeException(nameof(width), width.ToString());}
        if (height < 0) { throw new ArgumentOutOfRangeException(nameof(height), height.ToString());}

        var lines = MultiLineText.Split('\n');
        var selected = PadToSize(lines, x + width, y + height)         
            .Skip(y).Take(height)
            .Select(line => line.Substring(x, width));

        return TextRectangle.FromLines(selected.ToArray());
    }

    private static IEnumerable<string> PadToSize(IEnumerable<string> lines, int width, int height)
    {
        return lines
            .Concat(Enumerable.Repeat(string.Empty, height))  // make sure there are enough lines
            .Select(line => line.PadRight(width));            //make sure the lines are wide enough
    }

    public string MultiLineText { get; }
    public int Width { get; }
    public int Height  { get; }
}