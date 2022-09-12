namespace BankOcr.Console;

public class OcrEntryFile
{
    TextReader Source { get;  }

    public OcrEntryFile(TextReader source)
    {
        Source = source;
    }

    public IEnumerable<OcrEntry> ParseEntries()
    {
        return StreamLines(Source)
            .Chunk(4)
            .Select(TextRectangle.FromLines)
            .Select(OcrEntry.Parse);
    }

    static IEnumerable<string> StreamLines(TextReader reader)
    {
        var line = reader.ReadLine();
        while (line != null)
        {
            yield return line;
            line = reader.ReadLine();
        }
    }
}