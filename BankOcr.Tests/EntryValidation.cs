using BankOcr.Console;

namespace BankOcr.Tests;

class EntryValidation
{
    [Test]
    public void EntriesWithNineCharactersShouldBeValid()
    {
        OcrEntry.FromString("123456789").ValidateAccountNumber().ShouldBe(true);
    }

    [TestCase(0)]
    [TestCase(8)]
    [TestCase(10)]
    public void EntriesOfWrongLengthShouldBeInvalid(int length)
    {
        OcrEntry.FromString(new string('1', length)).ValidateAccountNumber().ShouldBe(false);
    }
}