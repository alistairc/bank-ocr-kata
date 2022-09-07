using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BankOcr.Tests.IntegrationTests;

class RunningTheProgram
{
    static readonly string Root = AppDomain.CurrentDomain.BaseDirectory;
    public static readonly string SeveralEntries = $"{Root}/TestFiles/SeveralEntries.txt";

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
        Result.StdOutText.ShouldContain("111111111");
        Result.StdOutText.ShouldContain("999999999");
    }

    record ProgramResult(int ExitCode, string StdOutText, string StdErrText);

    static ProgramResult RunProgramWithTestFile(string filePath)
    {
        var exeName = GetExeName();
        var allArgs = $"\"{filePath}\"";

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
