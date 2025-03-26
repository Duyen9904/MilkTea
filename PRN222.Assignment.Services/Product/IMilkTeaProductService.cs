using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN222.Assignment.Repositories.Entities;

namespace PRN222.Assignment.Services.Product
{
    public interface IMilkTeaProductService
    {
        List<MilkTeaProduct> GetMilkTeaProducts();
        MilkTeaProduct GetMilkTeaProductById(int id);
        void AddMilkTeaProduct(MilkTeaProduct milkTeaProduct);
        void UpdateMilkTeaProduct(MilkTeaProduct milkTeaProduct);
        void DeleteMilkTeaProduct(MilkTeaProduct milkTeaProduct);

    }
}
