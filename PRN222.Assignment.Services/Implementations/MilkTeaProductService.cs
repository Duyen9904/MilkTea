using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN222.Assignment.Repositories.Entities;
using PRN222.Assignment.Repositories.Repositories.Interface;
using PRN222.Assignment.Services.Interfaces;

namespace PRN222.Assignment.Services.Implementations
{
    public class MilkTeaProductService : IMilkTeaProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MilkTeaProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddMilkTeaProduct(MilkTeaProduct milkTeaProduct)
        {
            _unitOfWork.MilkTeaProducts.Add(milkTeaProduct);
            _unitOfWork.SaveAsync();
        }

        public void DeleteMilkTeaProduct(MilkTeaProduct milkTeaProduct)
        {
            _unitOfWork.MilkTeaProducts.Delete(milkTeaProduct);
            _unitOfWork.SaveAsync();
        }

        public MilkTeaProduct GetMilkTeaProductById(int id)
        {
            return _unitOfWork.MilkTeaProducts.GetById(id);
        }

        public List<MilkTeaProduct> GetMilkTeaProducts()
        {
            return _unitOfWork.MilkTeaProducts.GetAll(
           includes: m => m.Category).ToList();
        }

        public void UpdateMilkTeaProduct(MilkTeaProduct milkTeaProduct)
        {
            _unitOfWork.MilkTeaProducts.Update(milkTeaProduct);
            _unitOfWork.SaveAsync();
        }
    }
}
