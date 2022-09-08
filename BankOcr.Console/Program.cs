using BankOcr.Console;

//This is missing any validation or proper error handling,
//we assume success all the way.  It'll do for now!

var sourceFile = args[0];

var inputLines = StreamLines(sourceFile);
var output = Console.Out;

var entries = OcrEntryFile.ParseEntries(inputLines);
var lines = AccountNumberReport.ForEntries(entries);

foreach (var line in lines)
{
    output.WriteLine(line);
}

static IEnumerable<string> StreamLines(string sourceFile)
{
    using var input = File.OpenText(sourceFile);
    var line = input.ReadLine();
    while (line != null)
    {
        yield return line;
        line = input.ReadLine();
    }
}
