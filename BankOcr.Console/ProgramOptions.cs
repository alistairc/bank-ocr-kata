namespace BankOcr.Console;

public record ProgramOptions(string SourcePath, string? OutputPath)
{
    public static ProgramOptions ParseArgs(string[] commandLineArgs)
    {
        var sourcePath = commandLineArgs[0];
        var outputPath = commandLineArgs.Length > 1 ? commandLineArgs[1] : null;
        return new ProgramOptions(sourcePath, outputPath);
    }
}