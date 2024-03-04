namespace Domain.Models.Transactions;

public record TransactionDto(int AccountNumber, int Amount, TransactionCategory TransactionCategory);