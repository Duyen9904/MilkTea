using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PRN222.Assignment.Repositories.Entities;

namespace PRN222.Assignment.Services.Interfaces
{
    public interface IMilkTeaProductService
    {
        // Synchronous method to get all milk tea products with optional filtering, sorting, and pagination
        IEnumerable<MilkTeaProduct> GetAll(
            Expression<Func<MilkTeaProduct, bool>> filter = null,
            Func<IQueryable<MilkTeaProduct>, IOrderedQueryable<MilkTeaProduct>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<MilkTeaProduct, object>>[] includes);

        // Asynchronous method to get all milk tea products with optional filtering, sorting, and pagination
        Task<IEnumerable<MilkTeaProduct>> GetAllAsync(
            Expression<Func<MilkTeaProduct, bool>> filter = null,
            Func<IQueryable<MilkTeaProduct>, IOrderedQueryable<MilkTeaProduct>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<MilkTeaProduct, object>>[] includes);

        // Get milk tea product by its ID
        MilkTeaProduct GetById(int id);

        // Asynchronous method to get milk tea product by its ID
        Task<MilkTeaProduct> GetByIdAsync(int id);

        // Add a new milk tea product
        void AddMilkTeaProduct(MilkTeaProduct milkTeaProduct);

        // Asynchronous method to add a new milk tea product
        Task AddMilkTeaProductAsync(MilkTeaProduct milkTeaProduct);

        // Update an existing milk tea product
        void UpdateMilkTeaProduct(MilkTeaProduct milkTeaProduct);

        // Asynchronous method to update an existing milk tea product
        Task UpdateMilkTeaProductAsync(MilkTeaProduct milkTeaProduct);

        // Delete a milk tea product
        void DeleteMilkTeaProduct(MilkTeaProduct milkTeaProduct);

        // Asynchronous method to delete a milk tea product
        Task DeleteMilkTeaProductAsync(MilkTeaProduct milkTeaProduct);

        // Get milk tea products with their related category
        IEnumerable<MilkTeaProduct> GetMilkTeaProductsWithCategory(
            Expression<Func<MilkTeaProduct, bool>> filter = null,
            Func<IQueryable<MilkTeaProduct>, IOrderedQueryable<MilkTeaProduct>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null);

        // Asynchronous method to get milk tea products with their related category
        Task<IEnumerable<MilkTeaProduct>> GetMilkTeaProductsWithCategoryAsync(
            Expression<Func<MilkTeaProduct, bool>> filter = null,
            Func<IQueryable<MilkTeaProduct>, IOrderedQueryable<MilkTeaProduct>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null);

        // Search milk tea products by name, description, and category
       // Task<List<MilkTeaProduct>> Search(string productName, string description, string category);
    }
}
