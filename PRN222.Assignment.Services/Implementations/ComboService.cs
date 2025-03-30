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
    public class ComboService : IComboService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ComboService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Combo> GetCombos(
            Expression<Func<Combo, bool>> filter = null,
            Func<IQueryable<Combo>, IOrderedQueryable<Combo>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<Combo, object>>[] includes)
        {
            return _unitOfWork.Combos.GetAll(filter, orderBy, pageIndex, pageSize, includes);
        }

        public async Task<IEnumerable<Combo>> GetCombosAsync(
            Expression<Func<Combo, bool>> filter = null,
            Func<IQueryable<Combo>, IOrderedQueryable<Combo>> orderBy = null,
            int? pageIndex = null,
            int? pageSize = null,
            params Expression<Func<Combo, object>>[] includes)
        {
            return await _unitOfWork.Combos.GetAllAsync(filter, orderBy, pageIndex, pageSize, includes);
        }

        public Combo GetById(int id)
        {
            return _unitOfWork.Combos.GetById(id);
        }

        public async Task<Combo> GetByIdAsync(int id)
        {
            return await _unitOfWork.Combos.GetByIdAsync(id);
        }

        public void Add(Combo combo)
        {
            _unitOfWork.Combos.Add(combo);
            _unitOfWork.SaveAsync().GetAwaiter().GetResult();
        }

        public async Task AddAsync(Combo combo)
        {
            await _unitOfWork.Combos.AddAsync(combo);
            await _unitOfWork.SaveAsync();
        }

        public void Update(Combo combo)
        {
            _unitOfWork.Combos.Update(combo);
            _unitOfWork.SaveAsync().GetAwaiter().GetResult();
        }

        public async Task UpdateAsync(Combo combo)
        {
            await _unitOfWork.Combos.UpdateAsync(combo);
            await _unitOfWork.SaveAsync();
        }

        public void Delete(int id)
        {
            var combo = _unitOfWork.Combos.GetById(id);
            if (combo != null)
            {
                _unitOfWork.Combos.Delete(combo);
                _unitOfWork.SaveAsync().GetAwaiter().GetResult();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var combo = await _unitOfWork.Combos.GetByIdAsync(id);
            if (combo != null)
            {
                await _unitOfWork.Combos.DeleteAsync(combo);
                await _unitOfWork.SaveAsync();
            }
        }

        public IEnumerable<Combo> GetAvailableCombos()
        {
            return _unitOfWork.Combos.GetAll(c => c.IsAvailable == true);
        }

        public async Task<IEnumerable<Combo>> GetAvailableCombosAsync()
        {
            return await _unitOfWork.Combos.GetAllAsync(c => c.IsAvailable == true);
        }
    }
}
