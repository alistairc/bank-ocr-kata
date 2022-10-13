namespace BankOcr.Console;

public interface IStreamFinder
{
    TextReader ReadText(string path);
    TextWriter WriteText(string path);
}