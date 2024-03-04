using Application.Services.Models;
using Application.Services.Services.AdminServices;
using Presentation.Console.Chains;
using Presentation.Console.ResultTypes;
using Spectre.Console;

namespace Presentation.Console.Commands;

public class AdminAuthCommand : ICommand
{
    private readonly AdminAuthenticationService _adminAuthenticationService;
    private readonly AdminService _adminService;

    public AdminAuthCommand(AdminAuthenticationService adminAuthenticationService, AdminService adminService)
    {
        _adminAuthenticationService = adminAuthenticationService;
        _adminService = adminService;
    }

    public OperationResult Execute()
    {
        string pass = AnsiConsole.Ask<string>("Введите пароль админа:");

        if (_adminAuthenticationService.ValidateAdminPassword(pass) == new LoginResult.Success())
        {
            AnsiConsole.MarkupLine("[green]Вход выполнен успешно.[/]");

            AdminActionsHandlerChain adminActionsHandlerChain = new(_adminService);

            while (true)
            {
                string action = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Выберите действие:")
                        .AddChoices("Create new account", "Back"));

                if (action == "Back")
                    return new OperationResult.Success();

                ICommand? command = adminActionsHandlerChain.Start.Handle(action);

                OperationResultHandler.Handle(command?.Execute());
            }
        }

        AnsiConsole.MarkupLine("[red]Ошибка аутентификации. Пожалуйста, проверьте введенные данные.[/]");
        return new OperationResult.Failure();
    }
}