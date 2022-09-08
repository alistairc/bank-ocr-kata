namespace BankOcr.Console;

public static class OcrEntryFile
{
    public static IEnumerable<OcrEntry> ParseEntries(IEnumerable<string> inputLines)
    {
        var possibleEntries = inputLines.Chunk(4);
        foreach (var entryLines in possibleEntries)
        {
            var entryText = string.Join('\n', entryLines);
            var entry = OcrEntry.ParseCharacters(entryText);
            yield return entry;
        }
    } 
}