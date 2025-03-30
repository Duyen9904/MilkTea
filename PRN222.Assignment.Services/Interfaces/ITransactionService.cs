using PRN222.Assignment.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Assignment.Services.Interfaces
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> GetAll(
            Expression<Func<Transaction, bool>> filter = null,
            Func<IQueryable<Transaction>, IOrderedQueryable<Transaction>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<Transaction, object>>[] includes);
        Task<IEnumerable<Transaction>> GetAllAsync(
            Expression<Func<Transaction, bool>> filter = null,
            Func<IQueryable<Transaction>, IOrderedQueryable<Transaction>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<Transaction, object>>[] includes);
        Transaction GetById(int id);
        Task<Transaction> GetByIdAsync(int id);
        void Add(Transaction transaction);
        Task AddAsync(Transaction transaction);
        void Update(Transaction transaction);
        Task UpdateAsync(Transaction transaction);
        void Delete(Transaction transaction);
        Task DeleteAsync(Transaction transaction);
        IEnumerable<Transaction> GetTransactionsByAccountId(int accountId);
        Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId);
        IEnumerable<Transaction> GetTransactionsByOrderId(int orderId);
        Task<IEnumerable<Transaction>> GetTransactionsByOrderIdAsync(int orderId);
        decimal GetAccountBalance(int accountId);
        Task<decimal> GetAccountBalanceAsync(int accountId);
    }
}
