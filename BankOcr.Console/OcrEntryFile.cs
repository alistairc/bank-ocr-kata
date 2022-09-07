namespace BankOcr.Console;

public static class OcrEntryFile
{
    public static void ProcessInputText(IEnumerable<string> inputLines, TextWriter textWriter)
    {
        var possibleEntries = inputLines.Chunk(4);
        foreach (var entryLines in possibleEntries)
        {
            var entryText = string.Join('\n', entryLines);
            var entry = OcrEntry.ParseCharacters(entryText);
            textWriter.WriteLine(entry.FormatLine());
        }
    }
}