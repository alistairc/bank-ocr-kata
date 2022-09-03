using System.Diagnostics;

namespace BankOcr.Tests;

class RunningTheProgram
{
    [Test]
    public void ParsingADigit()
    {
        var result = RunProgramWithTestFile(TestFiles.Digit1);

        result.ExitCode.ShouldBe(0, result.StdErrText);
        result.StdOutText.ShouldBe("1" + Environment.NewLine);
    }

    static ProgramResult RunProgramWithTestFile(string filePath)
    {
        var allArgs = $"\"{filePath}\"";

        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "BankOcr.Console",
                Arguments = allArgs,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            }
        };
        try
        {
            process.Start();
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

    class TestFiles
    {
        static readonly string Root = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string Digit1 = $"{Root}/TestFiles/Digit1.txt";
    }

    record ProgramResult(int ExitCode, string StdOutText, string StdErrText);
}
