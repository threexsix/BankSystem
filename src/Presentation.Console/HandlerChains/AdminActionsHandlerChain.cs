using Application.Services.Services.AdminServices;
using Presentation.Console.Handlers;

namespace Presentation.Console.Chains;

public class AdminActionsHandlerChain
{
    private readonly AdminService _adminService;

    public AdminActionsHandlerChain(
        AdminService adminService)
    {
        _adminService = adminService;

        AdminCreateNewAccountHandler adminCreateNewAccountHandler = new(_adminService);

        Start = adminCreateNewAccountHandler;
    }

    public ICommandHandler Start { get; }
}