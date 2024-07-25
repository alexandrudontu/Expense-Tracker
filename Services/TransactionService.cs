using Expense_Tracker.Models;
using Expense_Tracker.Repositories.Interfaces;
using Expense_Tracker.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Expense_Tracker.Services
{
    public class TransactionService: ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _transactionRepository.GetAllAsync();
        }

        public async Task<Transaction> GetTransactionByIdAsync(int id)
        {
            return await _transactionRepository.GetByIdAsync(id);
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            await _transactionRepository.AddAsync(transaction);
        }

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            await _transactionRepository.UpdateAsync(transaction);
        }

        public async Task DeleteTransactionAsync(int id)
        {
            await _transactionRepository.DeleteAsync(id);
        }
    }
}
