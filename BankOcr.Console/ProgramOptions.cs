namespace BankOcr.Console;

record ProgramOptions
{
    ProgramOptions(bool isValid, string? sourcePath, string? outputPath)
    {
        SourcePath = sourcePath;
        OutputPath = outputPath;
        IsValid = isValid;
    }

    bool IsValid { get; }
    string? SourcePath { get; }
    string? OutputPath { get; }

    public static ProgramOptions ParseArgs(string[] commandLineArgs)
    {
        if (commandLineArgs.Length < 1)
        {
            return new ProgramOptions(false, null, null);
        }
        var sourcePath = commandLineArgs[0];
        var outputPath = commandLineArgs.Length > 1 ? commandLineArgs[1] : null;
        return new ProgramOptions(true, sourcePath, outputPath);
    }

    public T Accept<T>(IOptionsVisitor<T> visitor)
    {
        if (!IsValid)
        {
            return visitor.VisitInvalid();
        }
        if (OutputPath == null)
        {
            return visitor.VisitConsoleOnly(SourcePath!);
        }
        return visitor.VisitFileOutput(SourcePath!, OutputPath);
    }
}