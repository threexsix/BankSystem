using Presentation.Console.ResultTypes;

namespace Presentation.Console.Commands;

public interface ICommand
{
    OperationResult Execute();
}