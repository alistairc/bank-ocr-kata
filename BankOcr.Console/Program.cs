using System.Text;

using BankOcr.Console;

//This is missing any validation or proper error handling,
//we assume success all the way.  It'll do for now!

var sourcePath = args[0];
var outputPath = args[1];

using var inputReader = File.OpenText(sourcePath);
using var outputWriter = new StreamWriter(File.OpenWrite(outputPath), Encoding.UTF8);

var entries = new OcrEntryFile(inputReader)
    .ParseEntries();

var report = new AccountNumberReport(entries);
report.WriteTo(Console.Out);
report.WriteTo(outputWriter);
