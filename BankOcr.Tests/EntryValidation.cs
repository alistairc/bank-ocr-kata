using BankOcr.Console;

namespace BankOcr.Tests;

class EntryValidation
{
    [Test]
    public void EntriesWithNineCharactersShouldBeValid()
    {
        OcrEntry.FromAccountNumber("000000000")
            .IsValidAccountNumber
            .ShouldBe(true);
    }

    [TestCase(0)]
    [TestCase(8)]
    [TestCase(10)]
    public void EntriesOfWrongLengthShouldBeInvalid(int length)
    {
        OcrEntry.FromAccountNumber(new string('0', length))
            .IsValidAccountNumber
            .ShouldBe(false);
    }

    [TestCase("000000000")]
    [TestCase("000000051")]
    [TestCase("000000523")]
    [TestCase("711111111")]
    [TestCase("123456789")]
    [TestCase("490867715")]
    public void EntriesWithValidChecksumShouldBeValid(string entryText)
    {
        OcrEntry.FromAccountNumber(entryText)
            .IsValidAccountNumber
            .ShouldBe(true);
    }

    [TestCase("888888888")]
    [TestCase("490067715")]
    [TestCase("012345678")]
    public void EntriesWithInvalidChecksumShouldBeInvalid(string entryText)
    {
        OcrEntry.FromAccountNumber(entryText)
            .IsValidAccountNumber
            .ShouldBe(false);
    }
}