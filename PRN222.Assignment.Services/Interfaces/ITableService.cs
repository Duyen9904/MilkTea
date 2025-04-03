using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Assignment.Services.Interfaces
{
    public interface ITableService
    {
        Task<List<Table>> GetTablesAsync();
        Task<Table> GetTableByIdAsync(int id);
    }
}
