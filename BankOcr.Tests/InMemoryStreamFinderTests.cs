namespace BankOcr.Tests;

class InMemoryStreamFinderTests
{
    [Test]
    public void ShouldAllowWritingToANewLocation()
    {
        var finder = new InMemoryStreamFinder();
        var writer = finder.WriteText("example/output.txt");
        writer.WriteLine("hello world");

        var reader = finder.ReadText("example/output.txt");
        reader.ReadToEnd().ShouldBe("hello world" + Environment.NewLine);
    }

    [Test]
    public void ShouldAllowOverwritingAFile()
    {
        var finder = new InMemoryStreamFinder();
        var writer = finder.WriteText("example/output.txt");
        writer.WriteLine("hello world");
        var writer2 = finder.WriteText("example/output.txt");
        writer2.WriteLine("new content");

        var reader = finder.ReadText("example/output.txt");
        reader.ReadToEnd().ShouldBe("new content" + Environment.NewLine);
    }

    [Test]
    public void ShouldErrorWhenReadingAFileThatDoesNotExist()
    {
        var finder = new InMemoryStreamFinder();
        Should.Throw<FileNotFoundException>(() => finder.ReadText("example/output.txt"));
    }
}