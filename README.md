# bank-ocr-kata

This is the code kata, Bank OCR, from here https://code.joejag.com/coding-dojo/bank-ocr/, in C#

## Current State

Reads a entries from a file, and prints them out

```
    $ BankOcr.Console "BankOcr.Tests/TestFiles/SeveralEntries.txt"
    111111111
    222222222
    333333333
    .....
```
## Build and Test

This project uses the `dotnet` CLI tooling.  To build and run the tests:

```
    dotnet test
```

You can run the app locally with this
```
    dotnet run --project BankOcr.Console -- "BankOcr.Tests/TestFiles/SeveralEntries.txt"
```
See below for more details of CLI usage

To build a releasable version:

```
    dotnet publish --configuration=Release
```
The binaries are here: `/bin/Release/net6.0/publish/`


## Dependencies

This project uses .NET 6.0 and C# 10.  Installation of the .NET 6 SDK is required.  All other libraries should be restored by the build from nuget.org

Additional libraries:

Main App:
- none

Tests:
- NUnit 
- Shouldly
