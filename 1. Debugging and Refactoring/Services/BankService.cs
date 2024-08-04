using DebuggingAndRefactoringTask1.Data;
using DebuggingAndRefactoringTask1.Extensions;
using DebuggingAndRefactoringTask1.Interfaces;
using DebuggingAndRefactoringTask1.Models;

namespace DebuggingAndRefactoringTask1.Services
{
    public class BankService : IBankService
    {
        private readonly IRepository<Account> _repository;

        public BankService()
        {
            _repository = new AccountRepository();  //DI
        }

        public async Task<int> CreateAccount(string name)
        {
            var (isValid, reason) = name.IsValidName(); //Do some checking here also
            
            if (!isValid)
            {
                throw new Exception("Invalid account holder name");
            }

            var account = new Account(name.Trim());
            int accountNo = await _repository.AddAsync(account);
            return accountNo;
        }

        public async Task<Account?> GetAccount(int accountId)
        {
            return await _repository.GetByAccountNumberAsync(accountId);
        }

        public async Task<bool> UpdateAccount(Account account)
        {
            return await _repository.UpdateAsync(account);
        }
    }
}