namespace BankOcr.Console;

public static class OcrEntryFile
{
    public static IEnumerable<OcrEntry> ParseEntries(IEnumerable<string> inputLines)
    {
        return inputLines.Chunk(4)
            .Select(TextRectangle.FromLines)
            .Select(OcrEntry.Parse);
    } 
}