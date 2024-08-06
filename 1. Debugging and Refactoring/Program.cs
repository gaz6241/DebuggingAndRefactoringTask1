using DebuggingAndRefactoringTask1.Extensions;
using DebuggingAndRefactoringTask1.Interfaces;
using DebuggingAndRefactoringTask1.Models;
using DebuggingAndRefactoringTask1.Models.Enums;
using DebuggingAndRefactoringTask1.Services;

namespace BankingSystem
{
    class Program
    {
        private static readonly IBankService _bankService = new BankService();

        static async Task Main(string[] args)
        {
            bool userLoggedIn = true;

            while (userLoggedIn)
            {
                Console.WriteLine("\n1. Create Account");
                Console.WriteLine("2. Deposit Money");
                Console.WriteLine("3. Withdraw Money");
                Console.WriteLine("4. Display Account Details");
                Console.WriteLine("5. Transaction History");
                Console.WriteLine("6. Exit");
                Console.Write("\nEnter choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await CreateAccount();
                        break;
                    case "2":
                        await Deposit();
                        break;
                    case "3":
                        await Withdraw();
                        break;
                    case "4":
                        await DisplayAccountDetails();
                        break;
                    case "5":
                        await DisplayTransactions();
                        break;
                    case "6":
                        userLoggedIn = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        private static async Task CreateAccount()
        {
            Console.Write("Enter Account Holder Name: ");
            var name = Console.ReadLine();

            var (isValid, reason) = name.IsValidName();

            if (!isValid)
            {
                Console.WriteLine(reason);
                return;
            }

            int accountNo = await _bankService.CreateAccount(name);
            Console.WriteLine($"Success. Account Number is {accountNo}");
        }

        private static async Task Deposit()
        {
            Account account = await FindAccount();
            Console.Write("Enter Amount to Deposit: ");
            var inputValue = Console.ReadLine();

            if (!inputValue.IsValidAmount())
            {
                Console.WriteLine("Invalid amount");
                return;
            }

            decimal amount = decimal.Parse(inputValue);
            var (result, message) = account.Deposit(amount);

            if (result == TransactionResult.Fail)
            {
                Console.WriteLine(message);
                return;
            }

            var updated = await _bankService.UpdateAccount(account);

            if (!updated)
            {
                throw new Exception("An error has occurred updating the bank account");
            }

            Console.WriteLine($"{message}. New balance: {account.GetDisplayBalance()}");
        }

        private static async Task Withdraw()
        {
            Account account = await FindAccount();
            Console.Write("Enter Amount to Withdraw: ");
            var inputValue = Console.ReadLine();

            if (!inputValue.IsValidAmount())
            {
                Console.WriteLine("Invalid amount");
                return;
            }

            decimal amount = decimal.Parse(inputValue);
            var (result, message) = account.Withdraw(amount);

            if (result == TransactionResult.Fail)
            {
                Console.WriteLine(message);
                return;
            }

            var updated = await _bankService.UpdateAccount(account);

            if (!updated)
            {
                throw new Exception("An error has occurred updating the bank account");
            }

            Console.WriteLine($"{message}. New balance: {account.GetDisplayBalance()}");
        }

        private static async Task DisplayTransactions()
        {
            Account account = await FindAccount();
            IEnumerable<Transaction> transactions = account.GetTransactions();
            Console.WriteLine();

            foreach (var transaction in transactions)
            {
                Console.WriteLine($"{transaction.Created.FormatDate()} \t {transaction.Type} \t {transaction.Amount:0.00}");
            }

            Console.WriteLine($"\nBalance: {account.GetDisplayBalance()}");
        }

        private static async Task<Account> FindAccount()
        {
            bool searching = true;
            int accountId;
            Account account = null;

            while (searching)
            {
                Console.Write("\nEnter account no: ");
                var inputId = Console.ReadLine();

                if (!int.TryParse(inputId, out accountId))
                {
                    Console.WriteLine("Account number is not valid");
                }
                else
                {
                    var accountToFind = await _bankService.GetAccount(accountId);

                    if (accountToFind == null)
                    {
                        Console.WriteLine("Account not found");
                    }
                    else
                    {
                        searching = false;
                        account = accountToFind;
                    }
                }
            }

            return account;
        }

        private static async Task DisplayAccountDetails()
        {
            var account = await FindAccount();
            Console.WriteLine($"Account Holder: {account.GetName()}");
            Console.WriteLine($"Balance: {account.GetDisplayBalance()}");
        }
    }
}
