using Application.Services.Models;
using Application.Services.Services.AccountServices;
using Application.Services.Services.TransactionServices;
using Presentation.Console.Chains;
using Presentation.Console.ResultTypes;
using Spectre.Console;

namespace Presentation.Console.Commands;

public class AccountAuthCommand : ICommand
{
    private readonly AccountAuthenticationService _accountAuthenticationService;
    private readonly AccountService _accountService;
    private readonly TransactionService _transactionService;

    public AccountAuthCommand(AccountAuthenticationService accountAuthenticationService, TransactionService transactionService, AccountService accountService)
    {
        _accountAuthenticationService = accountAuthenticationService;
        _transactionService = transactionService;
        _accountService = accountService;
    }

    public OperationResult Execute()
    {
        int accountNumber = AnsiConsole.Ask<int>("Введите номер счета:");
        int pin = AnsiConsole.Ask<int>("Введите пин-код:");

        if (_accountAuthenticationService.AuthenticateAccount(accountNumber, pin) == new LoginResult.Success())
        {
            AnsiConsole.MarkupLine("[green]Вход выполнен успешно.[/]");

            AccountActionsHandlerChain accountActionsHandlerChain = new(_transactionService, _accountService, accountNumber);

            while (true)
            {
                string action = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Выберите действие:")
                        .AddChoices("Withdraw", "Deposit", "Show Balance", "Transaction History", "Back"));

                if (action == "Back")
                    return new OperationResult.Success();

                ICommand? command = accountActionsHandlerChain.Start.Handle(action);

                OperationResultHandler.Handle(command?.Execute());
            }
        }

        AnsiConsole.MarkupLine("[red]Ошибка аутентификации. Пожалуйста, проверьте введенные данные.[/]");
        return new OperationResult.Failure();
    }
}