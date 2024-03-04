using Application.Services.Services.AdminServices;
using Domain.Models.Accounts;
using Presentation.Console.ResultTypes;
using Spectre.Console;

namespace Presentation.Console.Commands;

public class AdminCreateNewAccountCommand : ICommand
{
    private readonly AdminService _adminService;

    public AdminCreateNewAccountCommand(AdminService adminService)
    {
        _adminService = adminService;
    }

    public OperationResult Execute()
    {
        int newAccountPin = AnsiConsole.Ask<int>("Введите пин-код для нового аккаунта:");
        int newAccountStartBalance = AnsiConsole.Ask<int>("Введите стартовый депозит для нового аккаунта:");
        var newAccountDto = new AccountDto(newAccountPin, newAccountStartBalance);
        _adminService.CreateAccount(newAccountDto);
        AnsiConsole.MarkupLine($"[green]Новый аккаунт успешно создан.[/]");
        return new OperationResult.Success();
    }
}