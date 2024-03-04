namespace Domain.Models.Transactions;

public record Transaction(int TransactionId, int AccountNumber, int Amount, TransactionCategory TransactionCategory);