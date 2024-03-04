using Application.Services.Services.AccountServices;
using Application.Services.Services.AdminServices;
using Application.Services.Services.TransactionServices;
using Infrastructure.DataAccess.Repository;
using Presentation.Console.Chains;

namespace ConsoleApplication;

public static class Program
{
    private static string _connectionString = "Host=localhost;Port=6432;Database=postgres;User Id=postgres;Password=postgres;";
    private static void Main()
    {
        var accountRepository = new PostgresAccountRepository(_connectionString);
        var transactionRepository = new PostgresTransactionRepository(_connectionString);

        var accountService = new AccountService(accountRepository);
        var transactionService = new TransactionService(transactionRepository, accountService);

        var authenticationService = new AccountAuthenticationService(accountRepository);
        var adminAuthenticationService = new AdminAuthenticationService("hohoho");
        var adminService = new AdminService(accountRepository);

        AuthHandlerChain authHandlerChain = new(
            adminService,
            adminAuthenticationService,
            transactionService,
            accountService,
            authenticationService);

        Presentation.Console.Application application = new(authHandlerChain);

        application.Run();
    }
}