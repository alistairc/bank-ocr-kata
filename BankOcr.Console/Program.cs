using System.Text;

using BankOcr.Console;

//This is missing any validation or proper error handling,
//we assume success all the way.  It'll do for now!

var sourcePath = args[0];
var outputPath = args[1];

var inputLines = StreamLines(sourcePath);

using var outputWriter = new StreamWriter(File.OpenWrite(outputPath), Encoding.UTF8);
var consoleOutput = Console.Out;

var entries = OcrEntryFile.ParseEntries(inputLines);

var report = new AccountNumberReport(entries);
report.WriteTo(consoleOutput);
report.WriteTo(outputWriter);

static IEnumerable<string> StreamLines(string filePath)
{
    using var input = File.OpenText(filePath);
    var line = input.ReadLine();
    while (line != null)
    {
        yield return line;
        line = input.ReadLine();
    }
}
