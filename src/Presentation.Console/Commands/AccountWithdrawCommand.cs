using Application.Services.Services.TransactionServices;
using Domain.Exceptions.TransactionExceptions;
using Presentation.Console.ResultTypes;
using Spectre.Console;

namespace Presentation.Console.Commands;

public class AccountWithdrawCommand : ICommand
{
    private readonly TransactionService _transactionService;
    private readonly int _accountNumber;

    public AccountWithdrawCommand(TransactionService transactionService, int accountNumber)
    {
        _transactionService = transactionService;
        _accountNumber = accountNumber;
    }

    public OperationResult Execute()
    {
        try
        {
            int withdrawAmount = AnsiConsole.Ask<int>("Введите сумму для снятия:");
            _transactionService.Withdraw(_accountNumber, withdrawAmount);
        }
        catch (NegativeAmountException ex)
        {
            AnsiConsole.MarkupLine($"[red]Ошибка: {ex.Message}[/]");
            return new OperationResult.Failure();
        }

        AnsiConsole.MarkupLine("[green]Снятие средств со счета выполнено успешно.[/]");
        return new OperationResult.Success();
    }
}