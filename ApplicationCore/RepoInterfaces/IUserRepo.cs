using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepoInterfaces
{
    public interface IUserRepo : IAsyncRepo<User>
    {
        Task<bool> UserExists(string email);
    }
}
