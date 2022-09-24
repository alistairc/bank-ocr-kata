namespace BankOcr.Console;

public class BankOcrProgram
{
    TextWriter StdOut { get; }
    IStreamFinder StreamFinder { get; }

    public BankOcrProgram(IStreamFinder streamFinder, TextWriter stdOut)
    {
        StreamFinder = streamFinder;
        StdOut = stdOut;
    }

    public void Run(ProgramOptions options)
    {
        using var inputReader = StreamFinder.ReadText(options.SourcePath);
        using var outputWriter = StreamFinder.WriteText(options.OutputPath);

        var entries = new OcrEntryFile(inputReader)
            .ParseEntries();

        var report = new AccountNumberReport(entries);
        report.WriteTo(StdOut);
        report.WriteTo(outputWriter);
    }
}