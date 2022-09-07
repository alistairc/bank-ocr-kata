using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BankOcr.Tests.IntegrationTests;

class RunningTheProgram
{
    [Test]
    public void ParsingADigit()
    {
        var result = RunProgramWithTestFile(KnownTestFiles.Digit1);

        result.ExitCode.ShouldBe(0, result.StdErrText);
        result.StdOutText.ShouldBe("1" + Environment.NewLine);
    }

    [Test]
    public void ParsingASingleEntry()
    {
        var result = RunProgramWithTestFile(KnownTestFiles.DigitsOneToNine);

        result.ExitCode.ShouldBe(0, result.StdErrText);
        result.StdOutText.ShouldBe("123456789" + Environment.NewLine);
    }

    [Test, Ignore("Not implemented yet")]
    public void ParsingMultipleEntries()
    {
        var result = RunProgramWithTestFile(KnownTestFiles.SeveralEntries);

        result.ExitCode.ShouldBe(0, result.StdErrText);

        var outputLines = result.StdOutText.Split('\n');
        outputLines[0].ShouldBe("111111111");
        outputLines[8].ShouldBe("999999999");
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
