using Application.Services.Services.TransactionServices;
using Presentation.Console.Commands;

namespace Presentation.Console.Handlers;

public class AccountWithdrawHandler : BaseCommandHandler
{
    private readonly TransactionService _transactionService;
    private readonly int _accountNumber;

    public AccountWithdrawHandler(TransactionService transactionService, int accountNumber)
    {
        _transactionService = transactionService;
        _accountNumber = accountNumber;
    }

    public override ICommand? Handle(string request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request == "Withdraw")
        {
            return new AccountWithdrawCommand(_transactionService, _accountNumber);
        }

        return base.Handle(request);
    }
}