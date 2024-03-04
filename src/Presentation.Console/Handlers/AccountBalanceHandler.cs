using Application.Services.Services.AccountServices;
using Application.Services.Services.TransactionServices;
using Presentation.Console.Commands;

namespace Presentation.Console.Handlers;

public class AccountBalanceHandler : BaseCommandHandler
{
    private readonly TransactionService _transactionService;
    private readonly AccountService _accountService;
    private readonly int _accountNumber;

    public AccountBalanceHandler(TransactionService transactionService, AccountService accountService, int accountNumber)
    {
        _transactionService = transactionService;
        _accountService = accountService;
        _accountNumber = accountNumber;
    }

    public override ICommand? Handle(string request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request == "Show Balance")
        {
            return new AccountBalanceCommand(_transactionService, _accountService, _accountNumber);
        }

        return base.Handle(request);
    }
}