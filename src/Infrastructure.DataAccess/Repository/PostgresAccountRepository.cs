using Domain.Models.Accounts;
using Domain.Services.Repositories;
using Npgsql;

namespace Infrastructure.DataAccess.Repository;

public class PostgresAccountRepository : IAccountRepository
{
    private readonly string _connectionString;

    public PostgresAccountRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void CreateAccount(AccountDto accountDto)
    {
        ArgumentNullException.ThrowIfNull(accountDto);

        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using (var command = new NpgsqlCommand(
                   """
                   INSERT INTO Accounts (Pin, Balance) 
                   VALUES (@Pin, @Balance) 
                   RETURNING AccountNumber
                   """,
                   connection))
        {
            command.Parameters.AddWithValue("@Pin", accountDto.Pin);
            command.Parameters.AddWithValue("@Balance", accountDto.Balance);

            long accountNumber = (long)(command.ExecuteScalar() ?? throw new InvalidOperationException());
        }
    }

    public Account? FindAccountById(long id)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using var command = new NpgsqlCommand(
            """
            SELECT * 
            FROM Accounts 
            WHERE AccountNumber = @Id
            """,
            connection);

        command.Parameters.AddWithValue("@Id", id);

        using NpgsqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Account(
                AccountNumber: reader.GetInt32(reader.GetOrdinal("AccountNumber")),
                Pin: reader.GetInt32(reader.GetOrdinal("Pin")),
                Balance: reader.GetInt32(reader.GetOrdinal("Balance")));
        }

        return null;
    }

    public IEnumerable<Account> GetAllAccounts()
    {
        throw new NotImplementedException();
    }
}