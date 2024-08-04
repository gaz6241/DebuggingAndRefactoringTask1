using DebuggingAndRefactoringTask1.Models.Enums;

namespace DebuggingAndRefactoringTask1.Models
{
    public class Account
    {
        private readonly string _name;
        private int _accountNumber;
        private decimal _balance;
        private List<Transaction> _transactions = new();

        public Account(string name)
        {
            _name = name;
        }

        public void SetAccountNo(int value)
        {
            _accountNumber = value;
        }

        public int GetAccountNo()
        {
            return _accountNumber;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetDisplayBalance()
        {
            return _balance.ToString("0.00");
        }

        public (TransactionResult, string) Deposit(decimal value)
        {
            if (value <= 0)
            {
                return (TransactionResult.Fail, "Invalid amount");
            }

            _balance += value;
            StoreTransaction(TransactionType.Deposit, value);
            return (TransactionResult.Success, "Success");
        }

        public (TransactionResult, string) Withdraw(decimal value)
        {
            if (value <= 0)
            {
                return (TransactionResult.Fail, "Invalid amount");
            }

            if (value > _balance)
            {
                return (TransactionResult.Fail, "Insufficent funds");
            }

            _balance -= value;
            StoreTransaction(TransactionType.Withdrawal, value);
            return (TransactionResult.Success, "Withdrawal successful");
        }

        private void StoreTransaction(TransactionType type, decimal value)
        {
            var trans = new Transaction(type,value);
            _transactions.Add(trans);
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            return _transactions.OrderBy(t => t.Created);
        }
    }
}
