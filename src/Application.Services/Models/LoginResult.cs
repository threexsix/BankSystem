namespace Application.Services.Models;

public abstract record LoginResult
{
    private LoginResult() { }

    public sealed record Success : LoginResult;

    public sealed record Failure : LoginResult;
}
