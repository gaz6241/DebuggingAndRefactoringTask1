using DebuggingAndRefactoringTask1.Models;

namespace DebuggingAndRefactoringTask1.Interfaces
{
    public interface IBankService
    {
        Task<int> CreateAccount(string name);
        Task<Account?> GetAccount(int accountId);
        Task<bool> UpdateAccount(Account account);
    }
}