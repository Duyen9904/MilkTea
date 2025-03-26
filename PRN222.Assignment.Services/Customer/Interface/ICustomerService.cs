using PRN222.Assignment.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.Assignment.Services.Customer.Interface
{
    public interface ICustomerService
    {
        Task<Account?> Login(string username, string password);
    }
}
