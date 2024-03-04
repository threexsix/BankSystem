using Application.Services.Services.TransactionServices;
using Presentation.Console.Commands;

namespace Presentation.Console.Handlers;

public class AccountDepositHandler : BaseCommandHandler
{
    private readonly TransactionService _transactionService;
    private readonly int _accountNumber;

    public AccountDepositHandler(TransactionService transactionService, int accountNumber)
    {
        _transactionService = transactionService;
        _accountNumber = accountNumber;
    }

    public override ICommand? Handle(string request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request == "Deposit")
        {
            return new AccountDepositCommand(_transactionService, _accountNumber);
        }

        return base.Handle(request);
    }
}