using System.Collections.Generic;
using Domain.Models.Accounts;
using Domain.Services.Repositories;

namespace Itmo.ObjectOrientedProgramming.Lab5.Tests;

public class MockAccountRepo : IAccountRepository
{
    private Account _account;

    public MockAccountRepo(Account account)
    {
        _account = account;
    }

    public void CreateAccount(AccountDto accountDto)
    {
        throw new System.NotImplementedException();
    }

    public Account? FindAccountById(long id)
    {
        return _account;
    }

    public IEnumerable<Account> GetAllAccounts()
    {
        throw new System.NotImplementedException();
    }
}