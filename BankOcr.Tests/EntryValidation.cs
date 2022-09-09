using BankOcr.Console;

namespace BankOcr.Tests;

class EntryValidation
{
    [Test]
    public void EntriesWithNineDigitsShouldBeValid()
    {
        OcrEntry.FromAccountNumber("000000000")
            .ValidationStatus
            .ShouldBe(EntryValidationStatus.Ok);
    }

    [TestCase(0)]
    [TestCase(8)]
    [TestCase(10)]
    public void EntriesOfWrongLengthShouldBeInvalid(int length)
    {
        OcrEntry.FromAccountNumber(new string('0', length))
            .ValidationStatus
            .ShouldBe(EntryValidationStatus.Invalid);
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
            .ValidationStatus
            .ShouldBe(EntryValidationStatus.Ok);
    }

    [TestCase("888888888")]
    [TestCase("490067715")]
    [TestCase("012345678")]
    public void EntriesWithInvalidChecksumShouldBeInvalid(string entryText)
    {
        OcrEntry.FromAccountNumber(entryText)
            .ValidationStatus
            .ShouldBe(EntryValidationStatus.Invalid);
    }
}