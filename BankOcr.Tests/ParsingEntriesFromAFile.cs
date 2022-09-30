using BankOcr.Console;

namespace BankOcr.Tests;

class ParsingEntriesFromAFile
{
    const string ExampleSource =
        @"                           
  |  |  |  |  |  |  |  |  |
  |  |  |  |  |  |  |  |  |

 _  _  _  _  _  _  _  _  _ 
 _| _| _| _| _| _| _| _| _|
|_ |_ |_ |_ |_ |_ |_ |_ |_ 

 _  _  _  _  _  _  _  _  _ 
 _| _| _| _| _| _| _| _| _|
 _| _| _| _| _| _| _| _| _|
";

    [Test]
    public void ShouldParseAndPrintMultipleEntries()
    {
        var file = new OcrEntryFile(new StringReader(ExampleSource));
        var entries = file.ParseEntries().ToArray();

        entries[0].AccountNumber.ShouldBe("111111111");
        entries[2].AccountNumber.ShouldBe("333333333");
    }
}