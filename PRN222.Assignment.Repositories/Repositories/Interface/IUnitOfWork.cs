using PRN222.Assignment.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Assignment.Repositories.Repositories.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<MilkTeaProduct> MilkTeaProducts { get; }
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<Account> Accounts { get; }
        IGenericRepository<Order> Orders { get; }
        IGenericRepository<OrderItem> OrderItems { get; }
        IGenericRepository<Topping> Toppings { get; }
        IGenericRepository<Size> Sizes { get; }
        IGenericRepository<OrderItemTopping> OrderItemsToppings { get; }
        IGenericRepository<ProductSize> ProductSizes { get; }
        IGenericRepository<Transaction> Transactions { get; }
        Task<int> SaveAsync();
    }
}
