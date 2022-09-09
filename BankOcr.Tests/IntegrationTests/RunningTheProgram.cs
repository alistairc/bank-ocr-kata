using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BankOcr.Tests.IntegrationTests;

class RunningTheProgram
{
    static readonly string Root = AppDomain.CurrentDomain.BaseDirectory;
    static readonly string SeveralEntries = $"{Root}/TestFiles/SeveralEntries.txt";
    static readonly string OutputFilePath = $"{Root}output.txt";


    ProgramResult Result { get; set; }

    [OneTimeSetUp]
    public void RunEndToEnd()
    {
        Result = RunProgramWithTestFile(SeveralEntries);
    }

    [Test]
    public void ShouldExitWithCodeZero()
    {
        Result.ExitCode.ShouldBe(0, Result.StdErrText);
    }

    [Test]
    public void ShouldOutputParsedEntries()
    {
        Result.StdOutText.ShouldContain("111111111 ERR");
        Result.StdOutText.ShouldContain("999999999 ERR");
    }

    [Test]
    public void ShouldOutputResultsToAFile()
    {
        var fileContents = File.ReadAllText(OutputFilePath);
        fileContents.ShouldContain("111111111 ERR");
        fileContents.ShouldContain("999999999 ERR");
    }

    record ProgramResult(int ExitCode, string StdOutText, string StdErrText);

    static ProgramResult RunProgramWithTestFile(string sourceFilePath)
    {
        var exeName = GetExeName();
        var allArgs = $"\"{sourceFilePath}\" \"{OutputFilePath}\"";

        var process = RunExe(exeName, allArgs);
        try
        {
            process.WaitForExit(10000);

            process.HasExited.ShouldBeTrue();

            var standardOutput = process.StandardOutput.ReadToEnd();
            var errorOutput = process.StandardError.ReadToEnd();

            return new ProgramResult(process.ExitCode, standardOutput, errorOutput);
        }
        finally
        {
            process.Kill();
        }
    }

    static Process RunExe(string exeName, string allArgs)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = exeName,
                Arguments = allArgs,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            }
        };
        process.Start();
        return process;
    }

    static string GetExeName()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return "BankOcr.Console.exe";
        }

        return "BankOcr.Console";
    }
}
