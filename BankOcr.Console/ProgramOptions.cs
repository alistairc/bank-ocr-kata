namespace BankOcr.Console;

public record ProgramOptions(string SourcePath, string OutputPath)
{
    public static ProgramOptions ParseArgs(string[] commandLineArgs)
    {
        var sourcePath = commandLineArgs[0];
        var outputPath = commandLineArgs[1];
        return new ProgramOptions(sourcePath, outputPath);
    }
}