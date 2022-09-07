using Newtonsoft.Json.Linq;

namespace BankOcr.Tests.IntegrationTests;

class KnownTestFiles
{
    static readonly string Root = AppDomain.CurrentDomain.BaseDirectory;
    public static readonly string Digit1 = $"{Root}/TestFiles/Digit1.txt";
    public static readonly string DigitsOneToNine = $"{Root}/TestFiles/DigitsOneToNine.txt";
    public static readonly string SeveralEntries = $"{Root}/TestFiles/SeveralEntries.txt";
}
