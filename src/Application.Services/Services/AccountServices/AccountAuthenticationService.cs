using Application.Services.Models;
using Domain.Models.Accounts;
using Domain.Services.Repositories;

namespace Application.Services.Services.AccountServices;

public class AccountAuthenticationService
{
    private readonly IAccountRepository _repository;

    public AccountAuthenticationService(IAccountRepository repository)
    {
        _repository = repository;
    }

    public LoginResult AuthenticateAccount(long id, int pin)
    {
        Account? account = _repository.FindAccountById(id);

        if (account is null || account.Pin != pin)
        {
            return new LoginResult.Failure();
        }

        return new LoginResult.Success();
    }
}