using PRN222.Assignment.Repositories.Entities;
using PRN222.Assignment.Repositories.Repositories.Interface;
using PRN222.Assignment.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Assignment.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Transaction> GetAll(
            Expression<Func<Transaction, bool>> filter = null,
            Func<IQueryable<Transaction>, IOrderedQueryable<Transaction>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<Transaction, object>>[] includes)
        {
            //var allIncludes = new List<Expression<Func<Transaction, object>>>();
            //allIncludes.Add(ps => ps.MilkTeaProduct);
            //if (includes != null && includes.Length > 0)
            //{
            //    allIncludes.AddRange(includes);
            //}
            return _unitOfWork.Transactions.GetAll(filter, orderBy, pageIndex, pageSize, includes);
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync(
            Expression<Func<Transaction, bool>> filter = null,
            Func<IQueryable<Transaction>, IOrderedQueryable<Transaction>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<Transaction, object>>[] includes)
        {
            return await _unitOfWork.Transactions.GetAllAsync(filter, orderBy, pageIndex, pageSize, includes);
        }

        public Transaction GetById(int id)
        {
            return _unitOfWork.Transactions.GetById(id);
        }

        public async Task<Transaction> GetByIdAsync(int id)
        {
            return await _unitOfWork.Transactions.GetByIdAsync(id);
        }

        public void Add(Transaction transaction)
        {
            _unitOfWork.Transactions.Add(transaction);
            _unitOfWork.SaveAsync().GetAwaiter().GetResult();
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _unitOfWork.Transactions.AddAsync(transaction);
            await _unitOfWork.SaveAsync();
        }

        public void Update(Transaction transaction)
        {
            _unitOfWork.Transactions.Update(transaction);
            _unitOfWork.SaveAsync().GetAwaiter().GetResult();
        }

        public async Task UpdateAsync(Transaction transaction)
        {
            await _unitOfWork.Transactions.UpdateAsync(transaction);
            await _unitOfWork.SaveAsync();
        }

        public void Delete(Transaction transaction)
        {
            _unitOfWork.Transactions.Delete(transaction);
            _unitOfWork.SaveAsync().GetAwaiter().GetResult();
        }

        public async Task DeleteAsync(Transaction transaction)
        {
            await _unitOfWork.Transactions.DeleteAsync(transaction);
            await _unitOfWork.SaveAsync();
        }

        public IEnumerable<Transaction> GetTransactionsByAccountId(int accountId)
        {
            return _unitOfWork.Transactions.GetAll(
                t => t.AccountId == accountId,
                q => q.OrderByDescending(t => t.TransactionDate)
            );
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId)
        {
            return await _unitOfWork.Transactions.GetAllAsync(
                t => t.AccountId == accountId,
                q => q.OrderByDescending(t => t.TransactionDate)
            );
        }

        public IEnumerable<Transaction> GetTransactionsByOrderId(int orderId)
        {
            return _unitOfWork.Transactions.GetAll(
                t => t.OrderId == orderId,
                q => q.OrderByDescending(t => t.TransactionDate)
            );
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByOrderIdAsync(int orderId)
        {
            return await _unitOfWork.Transactions.GetAllAsync(
                t => t.OrderId == orderId,
                q => q.OrderByDescending(t => t.TransactionDate)
            );
        }

        public decimal GetAccountBalance(int accountId)
        {
            var transactions = _unitOfWork.Transactions.GetAll(t => t.AccountId == accountId);

            decimal balance = 0;
            foreach (var transaction in transactions)
            {
                if (transaction.TransactionType == "Payment" || transaction.TransactionType == "Adjustment")
                {
                    balance -= transaction.Amount;
                }
                else if (transaction.TransactionType == "Deposit" || transaction.TransactionType == "Refund")
                {
                    balance += transaction.Amount;
                }
            }

            return balance;
        }

        public async Task<decimal> GetAccountBalanceAsync(int accountId)
        {
            var transactions = await _unitOfWork.Transactions.GetAllAsync(t => t.AccountId == accountId);

            decimal balance = 0;
            foreach (var transaction in transactions)
            {
                if (transaction.TransactionType == "Payment" || transaction.TransactionType == "Adjustment")
                {
                    balance -= transaction.Amount;
                }
                else if (transaction.TransactionType == "Deposit" || transaction.TransactionType == "Refund")
                {
                    balance += transaction.Amount;
                }
            }

            return balance;
        }
    }
}
