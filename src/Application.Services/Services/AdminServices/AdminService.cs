using Domain.Models.Accounts;
using Domain.Services.Repositories;

namespace Application.Services.Services.AdminServices;

public class AdminService
{
    private readonly IAccountRepository _accountRepository;

    public AdminService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public void CreateAccount(AccountDto accountDto)
    {
        _accountRepository.CreateAccount(accountDto);
    }
}