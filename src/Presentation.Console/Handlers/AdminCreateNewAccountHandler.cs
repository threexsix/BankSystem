using Application.Services.Services.AdminServices;
using Presentation.Console.Commands;

namespace Presentation.Console.Handlers;

public class AdminCreateNewAccountHandler : BaseCommandHandler
{
    private readonly AdminService _adminService;

    public AdminCreateNewAccountHandler(AdminService adminService)
    {
        _adminService = adminService;
    }

    public override ICommand? Handle(string request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request == "Create new account")
        {
            return new AdminCreateNewAccountCommand(_adminService);
        }

        return base.Handle(request);
    }
}