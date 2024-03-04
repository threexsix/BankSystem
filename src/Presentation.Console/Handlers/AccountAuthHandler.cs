using Application.Services.Services.AccountServices;
using Application.Services.Services.TransactionServices;
using Presentation.Console.Commands;

namespace Presentation.Console.Handlers;

public class AccountAuthHandler : BaseCommandHandler
{
    private readonly AccountAuthenticationService _accountAuthenticationService;
    private readonly TransactionService _transactionService;
    private readonly AccountService _accountService;

    public AccountAuthHandler(AccountAuthenticationService accountAuthenticationService, TransactionService transactionService, AccountService accountService)
    {
        _accountAuthenticationService = accountAuthenticationService;
        _transactionService = transactionService;
        _accountService = accountService;
    }

    public override ICommand? Handle(string request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request == "Account")
        {
            return new AccountAuthCommand(_accountAuthenticationService, _transactionService, _accountService);
        }

        return base.Handle(request);
    }
}