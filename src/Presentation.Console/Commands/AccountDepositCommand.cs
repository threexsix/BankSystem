using Application.Services.Services.TransactionServices;
using Domain.Exceptions.TransactionExceptions;
using Presentation.Console.ResultTypes;
using Spectre.Console;

namespace Presentation.Console.Commands;

public class AccountDepositCommand : ICommand
{
    private readonly TransactionService _transactionService;
    private readonly int _accountNumber;

    public AccountDepositCommand(TransactionService transactionService, int accountNumber)
    {
        _transactionService = transactionService;
        _accountNumber = accountNumber;
    }

    public OperationResult Execute()
    {
        try
        {
            int depositAmount = AnsiConsole.Ask<int>("Введите сумму для пополнения:");
            _transactionService.Deposit(_accountNumber, depositAmount);
        }
        catch (NegativeAmountException ex)
        {
            AnsiConsole.MarkupLine($"[red]Ошибка: {ex.Message}[/]");
            return new OperationResult.Failure();
        }

        AnsiConsole.MarkupLine("[green]Пополнение счета выполнено успешно.[/]");
        return new OperationResult.Success();
    }
}