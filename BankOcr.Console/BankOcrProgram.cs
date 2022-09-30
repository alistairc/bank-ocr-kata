using System.Text;

namespace BankOcr.Console;

public class BankOcrProgram
{
    public BankOcrProgram(IStreamFinder streamFinder, TextWriter stdOut)
    {
        StreamFinder = streamFinder;
        StdOut = stdOut;
    }

    TextWriter StdOut { get; }
    IStreamFinder StreamFinder { get; }

    public void Run(ProgramOptions options)
    {
        using var inputReader = StreamFinder.ReadText(options.SourcePath);
        using var outputWriter = StreamFinder.WriteText(options.OutputPath);

        var entries = new OcrEntryFile(inputReader)
            .ParseEntries();

        var report = new AccountNumberReport(entries);

        report.WriteTo(new CompositeWriter(StdOut, outputWriter));
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