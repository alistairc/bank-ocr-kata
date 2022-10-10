using BankOcr.Console;

var program = new BankOcrProgram(
    new FileSystemStreamFinder(),
    Console.Out
);

var options = ProgramOptions.ParseArgs(args);
return (int)program.Run(options);