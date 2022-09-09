	# bank-ocr-kata

This is the code kata, Bank OCR, from here https://code.joejag.com/coding-dojo/bank-ocr/, in C#

## Current State

Stories 1, 2 & 3 complete.  Reads a entries from a file, validates them, and and prints out a report.
Output is to the console and also a file (second arg)

```
    $ BankOcr.Console "BankOcr.Tests/TestFiles/MixedValidationStatus.txt" "output.txt"
	000000051
	49006771? ILL
	123456789
	012345678 ERR
```
## Build and Test

This project uses the `dotnet` CLI tooling.  To build and run the tests:

```
    dotnet test
```

You can run the app locally with this
```
    dotnet run --project BankOcr.Console -- "BankOcr.Tests/TestFiles/MixedValidationStatus.txt" "output.txt"
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
