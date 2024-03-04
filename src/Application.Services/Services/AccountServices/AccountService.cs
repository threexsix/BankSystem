using Domain.Models.Accounts;
using Domain.Services.Repositories;

namespace Application.Services.Services.AccountServices;

public class AccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public Account? GetAccountByNumber(int accountNumber)
    {
        return _accountRepository.FindAccountById(accountNumber);
    }

    public int GetAccountBalance(int accountNumber)
    {
        return GetAccountByNumber(accountNumber)?.Balance ?? 0;
    }
}