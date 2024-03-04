using Application.Services.Services.AdminServices;
using Presentation.Console.Commands;

namespace Presentation.Console.Handlers;

public class AdminAuthHandler : BaseCommandHandler
{
    private readonly AdminAuthenticationService _adminAuthenticationService;
    private readonly AdminService _adminService;

    public AdminAuthHandler(AdminAuthenticationService adminAuthenticationService, AdminService adminService)
    {
        _adminAuthenticationService = adminAuthenticationService;
        _adminService = adminService;
    }

    public override ICommand? Handle(string request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request == "Admin")
        {
            return new AdminAuthCommand(_adminAuthenticationService, _adminService);
        }

        return base.Handle(request);
    }
}