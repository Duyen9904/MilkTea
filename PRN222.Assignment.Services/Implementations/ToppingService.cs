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
    public class ToppingService : IToppingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ToppingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Topping> GetAll(
            Expression<Func<Topping, bool>> filter = null,
            Func<IQueryable<Topping>, IOrderedQueryable<Topping>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<Topping, object>>[] includes)
        {
            return _unitOfWork.Toppings.GetAll(filter, orderBy, pageIndex, pageSize, includes);
        }

        public async Task<IEnumerable<Topping>> GetAllAsync(
            Expression<Func<Topping, bool>> filter = null,
            Func<IQueryable<Topping>, IOrderedQueryable<Topping>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<Topping, object>>[] includes)
        {
            return await _unitOfWork.Toppings.GetAllAsync(filter, orderBy, pageIndex, pageSize, includes);
        }

        public Topping GetById(int id)
        {
            return _unitOfWork.Toppings.GetById(id);
        }

        public async Task<Topping> GetByIdAsync(int id)
        {
            return await _unitOfWork.Toppings.GetByIdAsync(id);
        }

        public void Add(Topping topping)
        {
            _unitOfWork.Toppings.Add(topping);
            _unitOfWork.SaveAsync().GetAwaiter().GetResult();
        }

        public async Task AddAsync(Topping topping)
        {
            await _unitOfWork.Toppings.AddAsync(topping);
            await _unitOfWork.SaveAsync();
        }

        public void Update(Topping topping)
        {
            _unitOfWork.Toppings.Update(topping);
            _unitOfWork.SaveAsync().GetAwaiter().GetResult();
        }

        public async Task UpdateAsync(Topping topping)
        {
            await _unitOfWork.Toppings.UpdateAsync(topping);
            await _unitOfWork.SaveAsync();
        }

        public void Delete(Topping topping)
        {
            _unitOfWork.Toppings.Delete(topping);
            _unitOfWork.SaveAsync().GetAwaiter().GetResult();
        }

        public async Task DeleteAsync(Topping topping)
        {
            await _unitOfWork.Toppings.DeleteAsync(topping);
            await _unitOfWork.SaveAsync();
        }

        public IEnumerable<Topping> GetAvailableToppings()
        {
            return _unitOfWork.Toppings.GetAll(
                t => t.IsAvailable == true,
                q => q.OrderBy(t => t.ToppingName)
            );
        }

        public async Task<IEnumerable<Topping>> GetAvailableToppingsAsync()
        {
            return await _unitOfWork.Toppings.GetAllAsync(
                t => t.IsAvailable == true,
                q => q.OrderBy(t => t.ToppingName)
            );
        }
    }
}
