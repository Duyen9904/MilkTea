using PRN222.Assignment.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Assignment.Services.Interfaces
{
    public interface IAccountService
    {
        Task<Account?> Login(string username, string password);
    }
}
