using Domain.Models.Accounts;

namespace Domain.Services.Repositories;

public interface IAccountRepository
{
    void CreateAccount(AccountDto accountDto);
    Account? FindAccountById(long id);
    IEnumerable<Account> GetAllAccounts();
}