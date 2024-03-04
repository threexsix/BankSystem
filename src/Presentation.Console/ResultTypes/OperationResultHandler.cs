namespace Presentation.Console.ResultTypes;

public static class OperationResultHandler
{
    public static void Handle(OperationResult? result)
    {
        if (result is OperationResult.Failure)
        {
            System.Console.WriteLine("The command wasn't executed successfully");
        }

        if (result is OperationResult.Success)
        {
            System.Console.WriteLine("The command was executed successfully");
        }
    }
}