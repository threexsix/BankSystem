using Domain.Models.Transactions;
using Domain.Services.Repositories;
using Npgsql;

namespace Infrastructure.DataAccess.Repository;

public class PostgresTransactionRepository : ITransactionRepository
{
    private readonly string _connectionString;

    public PostgresTransactionRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void CreateTransaction(TransactionDto transactionDto)
    {
        ArgumentNullException.ThrowIfNull(transactionDto);

        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using var command = new NpgsqlCommand(
                   """
                   INSERT 
                   INTO Transactions (AccountNumber, Amount, TransactionCategory) 
                   VALUES (@AccountNumber, @Amount, @TransactionCategory) 
                   RETURNING TransactionId
                   """,
                   connection);
        {
            command.Parameters.AddWithValue("@AccountNumber", transactionDto.AccountNumber);
            command.Parameters.AddWithValue("@Amount", transactionDto.Amount);
            command.Parameters.AddWithValue("@TransactionCategory", transactionDto.TransactionCategory.ToString());

            long transactionId = (long)(command.ExecuteScalar() ?? throw new InvalidOperationException());

            UpdateAccountBalance(transactionDto.AccountNumber, transactionDto.Amount, transactionDto.TransactionCategory);
        }
    }

    public IReadOnlyCollection<Transaction> GetAllAccountTransactions(int accountNumber)
    {
        var transactions = new List<Transaction>();

        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using var command = new NpgsqlCommand(
            """
            SELECT * 
            FROM Transactions 
            WHERE AccountNumber = @AccountNumber
            """,
            connection);

        command.Parameters.AddWithValue("@AccountNumber", accountNumber);

        using NpgsqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            transactions.Add(new Transaction(
                TransactionId: reader.GetInt32(reader.GetOrdinal("TransactionId")),
                AccountNumber: reader.GetInt32(reader.GetOrdinal("AccountNumber")),
                Amount: reader.GetInt32(reader.GetOrdinal("Amount")),
                TransactionCategory: Enum.Parse<TransactionCategory>(reader.GetString(reader.GetOrdinal("TransactionCategory")))));
        }

        return transactions;
    }

    private void UpdateAccountBalance(int accountNumber, int amount, TransactionCategory transactionCategory)
    {
        if (transactionCategory == TransactionCategory.Deposit)
        {
            DepositBalance(accountNumber, amount);
            return;
        }

        WithdrawBalance(accountNumber, amount);
    }

    private void WithdrawBalance(int accountNumber, int amount)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using var command = new NpgsqlCommand(
            """
            UPDATE Accounts 
            SET Balance = Balance - @Amount 
            WHERE AccountNumber = @AccountNumber
            """,
            connection);

        command.Parameters.AddWithValue("@Amount", amount);
        command.Parameters.AddWithValue("@AccountNumber", accountNumber);

        command.ExecuteNonQuery();
    }

    private void DepositBalance(int accountNumber, int amount)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using var command = new NpgsqlCommand(
            """
            UPDATE Accounts
            SET Balance = Balance + @Amount
            WHERE AccountNumber = @AccountNumber
            """,
            connection);

        command.Parameters.AddWithValue("@Amount", amount);
        command.Parameters.AddWithValue("@AccountNumber", accountNumber);

        command.ExecuteNonQuery();
    }
}