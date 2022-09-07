using BankOcr.Console;

//This is missing any validation or proper error handling,
//we assume success all the way.  It'll do for now!

var sourceFile = args[0];
var inputLines = File.ReadAllLines(sourceFile);
var output = Console.Out;

OcrEntryFile.ProcessInputText(inputLines, output);
