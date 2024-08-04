using DebuggingAndRefactoringTask1.Models;
using DebuggingAndRefactoringTask1.Models.Enums;

namespace TestProject1
{
    public class TransactionHistoryTests
    {
        [Fact]
        public void Add_New_Account_And_Verify_Transactions_Are_Empty()
        {
            // Arrange
            var accountHolder = "Angus Young";
            const int expectedCount = 0;

            // Act
            var account = new Account(accountHolder);
            var transactions = account.GetTransactions();
            var transactionCount = transactions.Count();

            // Assert
            Assert.Equal(expectedCount, transactionCount);
        }

        [Fact]
        public void Deposit_Value_And_Verify_Single_Transaction()
        {
            // Arrange
            var accountHolder = "Phil Collins";
            var account = new Account(accountHolder);
            const int expectedCount = 1;

            // Act
            var (result, message) = account.Deposit(5.50m);
            var transactions = account.GetTransactions();
            var transactionCount = transactions.Count();

            // Assert
            Assert.Equal(expectedCount, transactionCount);
        }

        [Fact]
        public void Deposit_Value_And_Verify_Transaction_Details_Are_Correct()
        {
            // Arrange
            var accountHolder = "Hanibal Smith";
            var account = new Account(accountHolder);
            const decimal expectAmount = 50;

            // Act
            var (result, message) = account.Deposit(50);
            var transaction = account.GetTransactions().Single();
            
            // Assert
            Assert.Equal(TransactionType.Deposit, transaction.Type);
            Assert.Equal(expectAmount, transaction.Amount);
            Assert.True(transaction.Created != DateTime.MinValue);
        }

        [Fact]
        public void Make_Multiple_Transactions_And_Verify_Transaction_Details_Are_Correct()
        {
            // Arrange
            var accountHolder = "Templeton Peck";
            var account = new Account(accountHolder);
            const int expectedCount = 3;
            const int expectedDeposits = 2;
            const int expectedWithdraws = 1;

            // Act
            account.Deposit(50);
            account.Withdraw(25);
            account.Deposit(75);
            var transactions = account.GetTransactions();
            var actualCount = transactions.Count();
            var numberOfDeposits = transactions.Where(t => t.Type == TransactionType.Deposit).Count();
            var numberOfWithdrawals = transactions.Where(t => t.Type == TransactionType.Withdrawal).Count();

            // Assert
            Assert.Equal(expectedCount, actualCount);
            Assert.Equal(expectedDeposits, numberOfDeposits);
            Assert.Equal(expectedWithdraws, numberOfWithdrawals);
        }
    }
}