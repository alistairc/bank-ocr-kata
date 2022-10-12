using System.Text;

namespace BankOcr.Console;

public class BankOcrProgram : IOptionsVisitor<ExitCode>
{
    public const string UsageMessage = "BankOcr.Console <input file> [<output file>]";

    public BankOcrProgram(IStreamFinder streamFinder, TextWriter stdOut)
    {
        StreamFinder = streamFinder;
        StdOut = stdOut;
    }

    TextWriter StdOut { get; }
    IStreamFinder StreamFinder { get; }

    public ExitCode VisitInvalid()
    {
        StdOut.WriteLine(UsageMessage);
        return ExitCode.InvalidArgs;
    }

    public ExitCode VisitConsoleOnly(string sourcePath)
    {
        using var inputReader = StreamFinder.ReadText(sourcePath);
        var report = CreateAccountNumberReport(inputReader);
        
        report.WriteTo(StdOut);
        
        return ExitCode.Success;
    }

    public ExitCode VisitFileOutput(string sourcePath, string outputPath)
    {
        using var inputReader = StreamFinder.ReadText(sourcePath);
        var report = CreateAccountNumberReport(inputReader);

        using var outputWriter = StreamFinder.WriteText(outputPath);
        report.WriteTo(new CompositeWriter(StdOut, outputWriter));

        return ExitCode.Success;
    }

    public ExitCode Run(string[] args)
    {
        var options = ProgramOptions.ParseArgs(args);
        return options.Accept(this);
    }

    AccountNumberReport CreateAccountNumberReport(TextReader inputReader)
    {
        var entries = new OcrEntryFile(inputReader)
            .ParseEntries();

        var report = new AccountNumberReport(entries);
        return report;
    }

    class CompositeWriter : TextWriter
    {
        public CompositeWriter(params TextWriter[] inners)
        {
            Inners = inners;
        }

        IReadOnlyCollection<TextWriter> Inners { get; }

        public override Encoding Encoding
        {
            get
            {
                return Inners
                    .Select(i => i.Encoding)
                    .FirstOrDefault(Encoding.Default);
            }
        }

        public override void Write(char value)
        {
            foreach (var writer in Inners)
            {
                writer.Write(value);
            }
        }
    }
}