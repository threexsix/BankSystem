using Application.Services.Services.TransactionServices;
using Presentation.Console.Commands;

namespace Presentation.Console.Handlers;

public class AccountHistoryHandler : BaseCommandHandler
{
    private readonly TransactionService _transactionService;
    private readonly int _accountNumber;

    public AccountHistoryHandler(TransactionService transactionService,  int accountNumber)
    {
        _transactionService = transactionService;
        _accountNumber = accountNumber;
    }

    public override ICommand? Handle(string request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request == "Transaction History")
        {
            return new AccountHistoryCommand(_transactionService, _accountNumber);
        }

        return base.Handle(request);
    }
}