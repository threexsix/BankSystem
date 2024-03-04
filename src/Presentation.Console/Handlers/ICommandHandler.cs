using Presentation.Console.Commands;

namespace Presentation.Console.Handlers;

public interface ICommandHandler
{
    void AddNext(ICommandHandler link);
    ICommand? Handle(string request);
}