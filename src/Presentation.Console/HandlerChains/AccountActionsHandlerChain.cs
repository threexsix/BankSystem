using Application.Services.Services.AccountServices;
using Application.Services.Services.TransactionServices;
using Presentation.Console.Handlers;

namespace Presentation.Console.Chains;

public class AccountActionsHandlerChain
{
    private readonly int _accountNumber;
    private readonly TransactionService _transactionService;
    private readonly AccountService _accountService;

    public AccountActionsHandlerChain(
        TransactionService transactionService,
        AccountService accountService,
        int accountNumber)
    {
        _transactionService = transactionService;
        _accountService = accountService;
        _accountNumber = accountNumber;

        AccountWithdrawHandler accountWithdrawHandler = new(_transactionService, _accountNumber);
        AccountDepositHandler accountDepositHandler = new(_transactionService, _accountNumber);
        AccountBalanceHandler accountBalanceHandler = new(_transactionService, _accountService, _accountNumber);
        AccountHistoryHandler accountHistoryHandler = new(_transactionService, _accountNumber);
        accountWithdrawHandler.AddNext(accountDepositHandler);
        accountDepositHandler.AddNext(accountBalanceHandler);
        accountBalanceHandler.AddNext(accountHistoryHandler);

        Start = accountWithdrawHandler;
    }

    public ICommandHandler Start { get; }
}