using ApplicationCore.Entities;
using ApplicationCore.RepoInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repo
{
    public class UserRepo : EfRepo<User>, IUserRepo
    {
        public UserRepo(TaskManagementDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<bool> UserExists(string email)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email == email.ToLower());
        }
    }
}
