using System.Collections.Generic;
using Domain.Models.Accounts;
using Domain.Models.Transactions;
using Domain.Services.Repositories;

namespace Itmo.ObjectOrientedProgramming.Lab5.Tests;

public class MockTransactionRepo : ITransactionRepository
{
    public MockTransactionRepo(Account account)
    {
        Balance = account.Balance;
    }

    public int Balance { get; set; }

    public void CreateTransaction(TransactionDto transactionDto)
    {
        if (transactionDto.TransactionCategory == TransactionCategory.Deposit)
        {
            Balance += transactionDto.Amount;
        }
        else
        {
            Balance -= transactionDto.Amount;
        }
    }

    public IReadOnlyCollection<Transaction> GetAllAccountTransactions(int accountNumber)
    {
        throw new System.NotImplementedException();
    }
}