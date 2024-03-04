using Application.Services.Services.AccountServices;
using Application.Services.Services.TransactionServices;
using Presentation.Console.ResultTypes;
using Spectre.Console;

namespace Presentation.Console.Commands;

public class AccountBalanceCommand : ICommand
{
    private readonly TransactionService _transactionService;
    private readonly AccountService _accountService;
    private readonly int _accountNumber;

    public AccountBalanceCommand(TransactionService transactionService, AccountService accountService, int accountNumber)
    {
        _transactionService = transactionService;
        _accountNumber = accountNumber;
        _accountService = accountService;
    }

    public OperationResult Execute()
    {
        int balance = _accountService.GetAccountBalance(_accountNumber);
        AnsiConsole.MarkupLine($"Баланс счета: [yellow]{balance}[/]");
        return new OperationResult.Success();
    }
}