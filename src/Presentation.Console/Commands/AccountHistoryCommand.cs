using Application.Services.Services.TransactionServices;
using Domain.Models.Transactions;
using Presentation.Console.ResultTypes;
using Spectre.Console;

namespace Presentation.Console.Commands;

public class AccountHistoryCommand : ICommand
{
    private readonly TransactionService _transactionService;
    private readonly int _accountNumber;

    public AccountHistoryCommand(TransactionService transactionService, int accountNumber)
    {
        _transactionService = transactionService;
        _accountNumber = accountNumber;
    }

    public OperationResult Execute()
    {
        IEnumerable<Transaction> transactions = _transactionService.GetAccountTransactions(_accountNumber);
        foreach (Transaction transaction in transactions)
        {
            AnsiConsole.MarkupLine($"Транзакция [yellow]{transaction.TransactionId}[/]: Сумма [yellow]{transaction.Amount}[/], Категория [yellow]{transaction.TransactionCategory}[/]");
        }

        return new OperationResult.Success();
    }
}