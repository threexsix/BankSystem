using Application.Services.Models;

namespace Application.Services.Services.AdminServices;

public class AdminAuthenticationService
{
    private readonly string _adminPassword;

    public AdminAuthenticationService(string adminPassword)
    {
        _adminPassword = adminPassword;
    }

    public LoginResult ValidateAdminPassword(string input)
    {
        if (_adminPassword == input)
            return new LoginResult.Success();

        return new LoginResult.Failure();
    }
}