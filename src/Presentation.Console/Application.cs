using Presentation.Console.Chains;
using Presentation.Console.Commands;
using Presentation.Console.ResultTypes;
using Spectre.Console;

namespace Presentation.Console;

public class Application
{
    private readonly AuthHandlerChain _authHandlerChain;
    public Application(AuthHandlerChain authHandlerChain)
    {
        _authHandlerChain = authHandlerChain;
    }

    public void Run()
    {
        while (true)
        {
            string option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose mod:")
                    .AddChoices("Account", "Admin", "Exit"));

            if (option == "Exit")
                break;

            ICommand? command = _authHandlerChain.Start.Handle(option);

            OperationResultHandler.Handle(command?.Execute());
        }
    }
}
