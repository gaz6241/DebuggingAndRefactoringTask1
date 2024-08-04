using DebuggingAndRefactoringTask1.Data;
using DebuggingAndRefactoringTask1.Models;

namespace TestProject1
{
    public class RepositoryTests
    {
        [Fact]
        public async Task Add_NewAccount_Produces_AccountNumber()
        {
            // Arrange
            var account = new Account("Sid Vicious");
            var repo = new AccountRepository();

            // Act
            var result = await repo.AddAsync(account);

            // Assert
            Assert.Equal(1000, result); //check that account number matches correct format
        }

        [Fact]
        public async Task Add_NewAccount_Produces_Expected_Name_And_Balance()
        {
            // Arrange
            var account = new Account("Kylie Minogue");
            var repo = new AccountRepository();

            // Act
            var accountNo = await repo.AddAsync(account);
            var kyliesAccount = await repo.GetByAccountNumberAsync(accountNo);

            // Assert
            Assert.Equal("0.00", kyliesAccount.GetDisplayBalance());
            Assert.Equal("Kylie Minogue", kyliesAccount.GetName());
        }

        [Fact]
        public async Task Update_Account_With_Incorrect_AccountNumber_Returns_False()
        {
            // Arrange
            var account = new Account("Sid Vicious");
            var repo = new AccountRepository();

            // Act
            var result = await repo.AddAsync(account);
            var bogusAccount = new Account("Elvis Presley");
            bogusAccount.SetAccountNo(2000);
            var updateResult = await repo.UpdateAsync(bogusAccount);

            // Assert
            Assert.False(updateResult); //a bogus account that hasn't been added to the system
        }

        [Fact]
        public async Task Account_Gets_Updated_As_Expected()
        {
            // Arrange
            var account = new Account("Bruce Springsteen");
            var repo = new AccountRepository();

            // Act
            var accountNo = await repo.AddAsync(account);
            account.Deposit(500);
            var updateResult = await repo.UpdateAsync(account);
            var updatedAccount = await repo.GetByAccountNumberAsync(accountNo);

            // Assert
            Assert.True(updateResult); //Has updated successfully
            Assert.Equal("500.00", updatedAccount.GetDisplayBalance()); //Balance has updated successfully
        }
    }
}