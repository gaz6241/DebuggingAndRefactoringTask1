using DebuggingAndRefactoringTask1.Models;
using DebuggingAndRefactoringTask1.Models.Enums;

namespace TestProject1
{
    public class AccountClassTests
    {
        [Fact]
        public void Add_New_Account_And_Verify_Initial_Values()  
        {
            // Arrange
            var accountHolder = "Johnny Rotten";

            // Act
            var account = new Account(accountHolder);

            // Assert
            Assert.Equal(accountHolder, account.GetName());
            Assert.Equal("0.00", account.GetDisplayBalance());
        }

        [Fact]
        public void Deposit_Value_And_Returns_Success()
        {
            // Arrange
            var accountHolder = "Snoop Dogg";
            var account = new Account(accountHolder);

            // Act
            var (result, message) = account.Deposit(5.50m);

            // Assert
            Assert.Equal(TransactionResult.Success, result);
            Assert.Equal("Success", message);
            Assert.Equal(accountHolder, account.GetName());
            Assert.Equal("5.50", account.GetDisplayBalance());
        }

        [Fact]
        public void Deposit_Two_Values_And_Are_Summed_Correctly()
        {
            // Arrange
            var accountHolder = "Britney Spears";
            var account = new Account(accountHolder);

            // Act
            account.Deposit(5.50m);
            var (result, message) = account.Deposit(5.50m);

            // Assert
            Assert.Equal(TransactionResult.Success, result);
            Assert.Equal("Success", message);
            Assert.Equal(accountHolder, account.GetName());
            Assert.Equal("11.00", account.GetDisplayBalance());
        }

        [Fact]
        public void Deposit_Value_And_Returns_InvalidAmount()
        {
            // Arrange
            var accountHolder = "Johnny Rotten";
            var account = new Account(accountHolder);

            // Act
            var (result, message) = account.Deposit(0m);

            // Assert
            Assert.Equal(TransactionResult.Fail, result);
            Assert.Equal("Invalid amount", message);
            Assert.Equal(accountHolder, account.GetName());
            Assert.Equal("0.00", account.GetDisplayBalance());
        }

        [Fact]
        public void Withdraw_Value_And_Returns_InsufficientFunds()
        {
            // Arrange
            var accountHolder = "Sheryl Crow";
            var account = new Account(accountHolder);

            // Act
            account.Deposit(100m);
            var (result, message) = account.Withdraw(200m);

            // Assert
            Assert.Equal(TransactionResult.Fail, result);
            Assert.Equal("Insufficent funds", message);
            Assert.Equal(accountHolder, account.GetName());
            Assert.Equal("100.00", account.GetDisplayBalance());
        }

        [Fact]
        public void Withdraw_Value_And_Balance_Is_What_Is_Expected()
        {
            // Arrange
            var accountHolder = "Kurt Cobain";
            var account = new Account(accountHolder);

            // Act
            account.Deposit(100m);
            var (result, message) = account.Withdraw(60m);

            // Assert
            Assert.Equal(TransactionResult.Success, result);
            Assert.Equal("Withdrawal successful", message);
            Assert.Equal(accountHolder, account.GetName());
            Assert.Equal("40.00", account.GetDisplayBalance());
        }
    }
}