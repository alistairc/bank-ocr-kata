using BankOcr.Console;

//This is missing any validation or proper error handling,
//we assume success all the way.  It'll do for now!

var program = new BankOcrProgram(
    new FileSystemStreamFinder(), 
    Console.Out
);

var options = ProgramOptions.ParseArgs(args);
program.Run(options);