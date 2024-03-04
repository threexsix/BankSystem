using Domain.Models.Transactions;

namespace Domain.Services.Repositories;

public interface ITransactionRepository
{
    void CreateTransaction(TransactionDto transactionDto);
    IReadOnlyCollection<Transaction> GetAllAccountTransactions(int accountNumber);
}