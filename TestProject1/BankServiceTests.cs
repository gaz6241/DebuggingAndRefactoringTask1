using DebuggingAndRefactoringTask1.Services;

namespace TestProject1
{
    public class BankServiceTests
    {
        [Fact]
        public async Task Create_Account_With_InvalidName_Throws_Exception()
        {
            // Arrange
            var accountHolder = "1234556"; //extra spaces
            var service = new BankService();

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(async () => await service.CreateAccount(accountHolder));

            // Assert
            Assert.Equal("Invalid account holder name", exception.Message);
        }

        [Fact]
        public async Task Add_NewAccount_Produces_CorrectName_WithNoSpaces()
        {
            // Arrange
            var accountHolder = "Dannii Minogue   "; //extra spaces
            var service = new BankService();

            // Act
            var accountNo = await service.CreateAccount(accountHolder);
            var account = await service.GetAccount(accountNo);

            // Assert
            Assert.Equal("Dannii Minogue", account.GetName()); //spaces removed
        }

        [Fact]
        public async Task Account_Gets_Updated_AsExpected()
        {
            // Arrange
            var accountHolder = "Bono";
            var service = new BankService();
            const decimal depositMoney = 500m;

            // Act
            var accountNo = await service.CreateAccount(accountHolder);
            var account = await service.GetAccount(accountNo);
            account.Deposit(depositMoney);
            await service.UpdateAccount(account);
            var updatedAccount = await service.GetAccount(accountNo);
            
            // Assert
            Assert.Equal("Bono", account.GetName());
            Assert.Equal(depositMoney.ToString("0.00"), account.GetDisplayBalance());
        }
    }
}