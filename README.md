# bank-ocr-kata

This is the code kata, Bank OCR, from here https://code.joejag.com/coding-dojo/bank-ocr/, in C#

## Current State

Currently this is just an empty skeleton.  Current just says "Hello, World!"

## Build and Test

This project uses the `dotnet` CLI tooling.  To build and run the tests:

```
    dotnet test
```

You can run the app locally with this
```
    dotnet run --project BankOcr.Console
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

## CLI usage

TODO: Fill this out once there's something worth running!
