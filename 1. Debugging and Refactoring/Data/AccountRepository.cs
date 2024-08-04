using DebuggingAndRefactoringTask1.Interfaces;
using DebuggingAndRefactoringTask1.Models;

namespace DebuggingAndRefactoringTask1.Data
{

    public class AccountRepository : IRepository<Account>
    {
        private readonly List<Account> _bankAccounts = new();

        public async Task<Account?> GetByAccountNumberAsync(int accountNo)
        {
            var account = await Task.FromResult(_bankAccounts.SingleOrDefault(b => b.GetAccountNo() == accountNo));
            return account;
        }

        public async Task<int> AddAsync(Account bankAccount)
        {
            int newAccountNo = GenerateNewAccountNo();
            bankAccount.SetAccountNo(newAccountNo);
            _bankAccounts.Add(bankAccount);
            return newAccountNo;
        }

        public async Task<bool> UpdateAsync(Account bankAccount)
        {
            var existingAccount = _bankAccounts.SingleOrDefault(b => b.GetAccountNo() == bankAccount.GetAccountNo());

            if (existingAccount == null)
            {
                return false; //could/should throw error 
            }

            existingAccount = bankAccount;
            return true;
        }

        public async Task DeleteAsync(int accountNo)
        {
            throw new NotImplementedException();
        }

        private int GenerateNewAccountNo()
        {
            return _bankAccounts.Count + 1 * 1000;
        }
    }
}