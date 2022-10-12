namespace BankOcr.Console;

interface IOptionsVisitor<out T>
{
    T VisitInvalid();
    T VisitConsoleOnly(string sourcePath);
    T VisitFileOutput(string sourcePath, string outputPath);
}