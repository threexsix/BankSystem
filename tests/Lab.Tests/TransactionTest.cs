using Application.Services.Services.AccountServices;
using Application.Services.Services.TransactionServices;
using Domain.Exceptions.TransactionExceptions;
using Domain.Models.Accounts;
using Domain.Models.Transactions;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab5.Tests;

public class TransactionTest
{
    [Fact]
    public void CreateTransactionDepositShouldUpdateAccountBalanceCorrectly()
    {
        // Arrange
        var account = new Account(1, 1, 15);
        var transactionRepository = new MockTransactionRepo(account);
        var transactionDto = new TransactionDto(1, 100, TransactionCategory.Deposit);
        var accountRepository = new MockAccountRepo(account);
        var accountService = new AccountService(accountRepository);
        var transactionService = new TransactionService(transactionRepository, accountService);

        // Act
        transactionService.Deposit(transactionDto.AccountNumber, transactionDto.Amount);

        // Assert
        Assert.Equal(115, transactionRepository.Balance);
    }

    [Fact]
    public void CreateTransactionWithdrawShouldUpdateAccountBalanceCorrectly()
    {
        // Arrange
        var account = new Account(1, 1, 150);
        var transactionRepository = new MockTransactionRepo(account);
        var transactionDto = new TransactionDto(1, 100, TransactionCategory.Withdraw);
        var accountRepository = new MockAccountRepo(account);
        var accountService = new AccountService(accountRepository);
        var transactionService = new TransactionService(transactionRepository, accountService);

        // Act
        transactionService.Withdraw(transactionDto.AccountNumber, transactionDto.Amount);

        // Assert
        Assert.Equal(50, transactionRepository.Balance);
    }

    [Fact]
    public void CreateTransactionDepositNegativeAmountShouldThrowException()
    {
        // Arrange
        var account = new Account(1, 1, 15);
        var transactionRepository = new MockTransactionRepo(account);
        var transactionDto = new TransactionDto(1, -100, TransactionCategory.Deposit);
        var accountRepository = new MockAccountRepo(account);
        var accountService = new AccountService(accountRepository);
        var transactionService = new TransactionService(transactionRepository, accountService);

        // Act
        NegativeAmountException ex = Assert.Throws<NegativeAmountException>(() =>
        {
            transactionService.Deposit(transactionDto.AccountNumber, transactionDto.Amount);
        });

        // Assert
        Assert.Equal("amount must be positive", ex.Message);
    }

    [Fact]
    public void CreateTransactionWithdrawNegativeAmountShouldThrowException()
    {
        // Arrange
        var account = new Account(1, 1, 15);
        var transactionRepository = new MockTransactionRepo(account);
        var transactionDto = new TransactionDto(1, -100, TransactionCategory.Withdraw);
        var accountRepository = new MockAccountRepo(account);
        var accountService = new AccountService(accountRepository);
        var transactionService = new TransactionService(transactionRepository, accountService);

        // Act
        NegativeAmountException ex = Assert.Throws<NegativeAmountException>(() =>
        {
            transactionService.Withdraw(transactionDto.AccountNumber, transactionDto.Amount);
        });

        // Assert
        Assert.Equal("amount must be positive", ex.Message);
    }

    [Fact]
    public void CreateTransactionWithdrawMoreThanAccountBalanceShouldThrowException()
    {
        // Arrange
        var account = new Account(1, 1, 15);
        var transactionRepository = new MockTransactionRepo(account);
        var transactionDto = new TransactionDto(1, 100, TransactionCategory.Withdraw);
        var accountRepository = new MockAccountRepo(account);
        var accountService = new AccountService(accountRepository);
        var transactionService = new TransactionService(transactionRepository, accountService);

        // Act
        NegativeAmountException ex = Assert.Throws<NegativeAmountException>(() =>
        {
            transactionService.Withdraw(transactionDto.AccountNumber, transactionDto.Amount);
        });

        // Assert
        Assert.Equal("not enough money to withdraw", ex.Message);
    }
}