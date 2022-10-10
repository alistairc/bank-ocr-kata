using System.Text;
using BankOcr.Console;

namespace BankOcr.Tests;

class InMemoryStreamFinder : IStreamFinder
{
    // lookup of path to file content
    Dictionary<string, StringBuilder> FileContentLookup { get; } = new();

    public TextReader ReadText(string path)
    {
        if (!FileContentLookup.ContainsKey(path))
        {
            throw new FileNotFoundException("File not found", path);
        }
        return new StringReader(FileContentLookup[path].ToString());
    }

    public TextWriter WriteText(string path)
    {
        var builder = new StringBuilder();
        FileContentLookup[path] = builder;
        return new StringWriter(builder);
    }

    public void SetupFile(string path, string content)
    {
        FileContentLookup[path] = new StringBuilder(content);
    }

    public string? GetFile(string path)
    {
        FileContentLookup.TryGetValue(path, out var content);
        return content?.ToString();
    }
}