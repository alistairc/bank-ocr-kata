using BankOcr.Console;

var program = new BankOcrProgram(
    new FileSystemStreamFinder(),
    Console.Out
);

return (int)program.Run(args);