using System.Text;

namespace BankOcr.Console;

public class BankOcrProgram
{
    public const string UsageMessage = "BankOcr.Console <input file> [<output file]";

    public BankOcrProgram(IStreamFinder streamFinder, TextWriter stdOut)
    {
        StreamFinder = streamFinder;
        StdOut = stdOut;
    }

    TextWriter StdOut { get; }
    IStreamFinder StreamFinder { get; }

    public ExitCode Run(string[] args)
    {
        var options = ProgramOptions.ParseArgs(args);
        if (!options.IsValid)
        {
            StdOut.WriteLine(UsageMessage);
            return ExitCode.InvalidArgs;
        }
        using var inputReader = StreamFinder.ReadText(options.SourcePath);

        var entries = new OcrEntryFile(inputReader)
            .ParseEntries();

        var report = new AccountNumberReport(entries);

        if (options.OutputPath == null)
        {
            report.WriteTo(StdOut);
        }
        else
        {
            using var outputWriter = StreamFinder.WriteText(options.OutputPath);
            report.WriteTo(new CompositeWriter(StdOut, outputWriter));
        }
        return ExitCode.Success;
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