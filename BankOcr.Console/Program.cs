using BankOcr.Console;

//This is missing any validation or proper error handling,
//we assume success all the way.  It'll do for now!

var sourceFile = args[0];
var inputLines = File.ReadAllLines(sourceFile);

var possibleEntries = inputLines.Chunk(4);

foreach (var entryLines in possibleEntries)
{
    var entryText = string.Join('\n', entryLines);

    var entry = OcrEntry.ParseCharacters(entryText);
    Console.WriteLine(entry.FormatLine());
}
