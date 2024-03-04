using Application.Services.Services.AccountServices;
using Application.Services.Services.AdminServices;
using Application.Services.Services.TransactionServices;
using Presentation.Console.Handlers;

namespace Presentation.Console.Chains;

public class AuthHandlerChain
{
    private readonly AccountAuthenticationService _authenticationService;
    private readonly AccountService _accountService;
    private readonly TransactionService _transactionService;
    private readonly AdminAuthenticationService _adminAuthenticationService;
    private readonly AdminService _adminService;

    public AuthHandlerChain(
        AdminService adminService,
        AdminAuthenticationService adminAuthenticationService,
        TransactionService transactionService,
        AccountService accountService,
        AccountAuthenticationService authenticationService)
    {
        _adminService = adminService;
        _adminAuthenticationService = adminAuthenticationService;
        _transactionService = transactionService;
        _accountService = accountService;
        _authenticationService = authenticationService;

        AccountAuthHandler accountAuthHandler = new(_authenticationService, _transactionService, _accountService);
        AdminAuthHandler adminAuthHandler = new(_adminAuthenticationService, _adminService);
        accountAuthHandler.AddNext(adminAuthHandler);
        Start = accountAuthHandler;
    }

    public ICommandHandler Start { get; }
}