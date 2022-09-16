using System.Text;

namespace BankOcr.Console;

class FileSystemStreamFinder : IStreamFinder
{
    public TextReader ReadText(string path)
    {
        return File.OpenText(path);
    }

    public TextWriter WriteText(string path)
    {
        return new StreamWriter(File.OpenWrite(path), Encoding.UTF8);
    }
}