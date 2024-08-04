namespace DebuggingAndRefactoringTask1.Interfaces
{
    using DebuggingAndRefactoringTask1.Models;
    using System.Threading.Tasks;

    internal interface IRepository<T>
    {
        Task<T?> GetByAccountNumberAsync(int accountNo);
        Task<int> AddAsync(T bankAccount);
        Task<bool> UpdateAsync(Account bankAccount);
        Task DeleteAsync(int accountNo);
    }
}
