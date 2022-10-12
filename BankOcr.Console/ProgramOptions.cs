namespace BankOcr.Console;

record ProgramOptions
{
    ProgramOptions(bool isValid, string? sourcePath, string? outputPath)
    {
        SourcePath = sourcePath!;
        OutputPath = outputPath;
        IsValid = isValid;
    }

    public bool IsValid { get; }
    public string SourcePath { get; }
    public string? OutputPath { get; }

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
}