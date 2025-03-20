using PRN222.Assignment.Repositories.Models;
using PRN222.Assignment.Repositories.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Assignment.Repositories.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MilkTeaShopContext _context;
        private bool _disposed = false;

        private IGenericRepository<MilkTeaProduct>? _milkTeaProducts;
        private IGenericRepository<Category>? _categories;
        private IGenericRepository<Account>? _accounts;
        private IGenericRepository<Order>? _orders;
        private IGenericRepository<OrderItem>? _orderItems;
        private IGenericRepository<Topping>? _toppings;
        private IGenericRepository<Size>? _sizes;
        private IGenericRepository<OrderItemTopping>? _orderItemsToppings;
        private IGenericRepository<ProductSize>? _productSizes;
        private IGenericRepository<Transaction>? _transactions;

        public UnitOfWork(MilkTeaShopContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IGenericRepository<MilkTeaProduct> MilkTeaProducts
            => _milkTeaProducts ??= new GenericRepository<MilkTeaProduct>(_context);

        public IGenericRepository<Category> Categories
            => _categories ??= new GenericRepository<Category>(_context);

        public IGenericRepository<Account> Accounts
            => _accounts ??= new GenericRepository<Account>(_context);

        public IGenericRepository<Order> Orders
            => _orders ??= new GenericRepository<Order>(_context);

        public IGenericRepository<OrderItem> OrderItems
            => _orderItems ??= new GenericRepository<OrderItem>(_context);

        public IGenericRepository<Topping> Toppings
            => _toppings ??= new GenericRepository<Topping>(_context);

        public IGenericRepository<Size> Sizes
            => _sizes ??= new GenericRepository<Size>(_context);

        public IGenericRepository<OrderItemTopping> OrderItemsToppings
            => _orderItemsToppings ??= new GenericRepository<OrderItemTopping>(_context);

        public IGenericRepository<ProductSize> ProductSizes
            => _productSizes ??= new GenericRepository<ProductSize>(_context);

        public IGenericRepository<Transaction> Transactions
            => _transactions ??= new GenericRepository<Transaction>(_context);

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
