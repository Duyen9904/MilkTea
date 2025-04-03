using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PRN222.Assignment.Repositories.Repositories.Interface;
using PRN222.Assignment.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Assignment.Services.Implementations
{
    public class TableService : ITableService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TableService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Table> GetTableByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Table>> GetTablesAsync()
        {
            throw new NotImplementedException();
        }

    }
}
