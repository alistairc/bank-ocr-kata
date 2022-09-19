using System.Text;

using BankOcr.Console;

//This is missing any validation or proper error handling,
//we assume success all the way.  It'll do for now!

var options = ProgramOptions.ParseArgs(args);

using var inputReader = File.OpenText(options.SourcePath);
using var outputWriter = new StreamWriter(File.OpenWrite(options.OutputPath), Encoding.UTF8);

var entries = new OcrEntryFile(inputReader)
    .ParseEntries();

var report = new AccountNumberReport(entries);
report.WriteTo(Console.Out);
report.WriteTo(outputWriter);
