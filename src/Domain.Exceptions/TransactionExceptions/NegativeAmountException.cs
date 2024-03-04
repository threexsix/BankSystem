namespace Domain.Exceptions.TransactionExceptions;

public class NegativeAmountException : Exception
{
    public NegativeAmountException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public NegativeAmountException(string message)
        : base(message)
    {
    }

    public NegativeAmountException()
    {
    }
}