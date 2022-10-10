namespace BankOcr.Console;

public record ProgramOptions
{
    public static ProgramOptions Invalid = new();

    public ProgramOptions(string sourcePath, string? outputPath)
    {
        SourcePath = sourcePath;
        OutputPath = outputPath;
        IsValid = true;
    }

    ProgramOptions()
    {
        IsValid = false;
        SourcePath = null!;
        OutputPath = null;
    }

    public bool IsValid { get; }
    public string SourcePath { get; }
    public string? OutputPath { get; }

    public static ProgramOptions ParseArgs(string[] commandLineArgs)
    {
        if (commandLineArgs.Length < 1)
        {
            return new ProgramOptions();
        }
        var sourcePath = commandLineArgs[0];
        var outputPath = commandLineArgs.Length > 1 ? commandLineArgs[1] : null;
        return new ProgramOptions(sourcePath, outputPath);
    }
}