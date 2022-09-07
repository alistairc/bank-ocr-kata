using BankOcr.Console;

//This is missing any validation or proper error handling,
//we assume success all the way.  It'll do for now!

var sourceFile = args[0];
var inputLines = File.ReadAllLines(sourceFile);

//A bit nasty, forces a read of the whole file
//but we'll do something smarter once we're dealing with multpile account numbers 
var allLines = string.Join('\n', inputLines);

var entry = OcrEntry.ParseCharacters(allLines);
Console.WriteLine(entry.FormatLine());
