using DebuggingAndRefactoringTask1.Models.Enums;

namespace DebuggingAndRefactoringTask1.Models
{
    public class Transaction
    {
        public Transaction(TransactionType type, decimal amount)
        {
            Created = DateTime.UtcNow;
            Type = type;
            Amount = amount;
        }

        public DateTime Created { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
    }
}