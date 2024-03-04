using Application.Services.Services.AccountServices;
using Domain.Exceptions.TransactionExceptions;
using Domain.Models.Transactions;
using Domain.Services.Repositories;

namespace Application.Services.Services.TransactionServices;

public class TransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly AccountService _accountService;

    public TransactionService(ITransactionRepository transactionRepository, AccountService accountService)
    {
        _transactionRepository = transactionRepository;
        _accountService = accountService;
    }

    public void Withdraw(int accountNumber, int amount)
    {
        if (amount < 0)
            throw new NegativeAmountException("amount must be positive");

        if (_accountService.GetAccountBalance(accountNumber) < amount)
            throw new NegativeAmountException("not enough money to withdraw");

        _transactionRepository.CreateTransaction(new TransactionDto(accountNumber, amount, TransactionCategory.Withdraw));
    }

    public void Deposit(int accountNumber, int amount)
    {
        if (amount < 0)
            throw new NegativeAmountException("amount must be positive");
        _transactionRepository.CreateTransaction(new TransactionDto(accountNumber, amount, TransactionCategory.Deposit));
    }

    public IEnumerable<Transaction> GetAccountTransactions(int accountNumber)
    {
        return _transactionRepository.GetAllAccountTransactions(accountNumber);
    }
}