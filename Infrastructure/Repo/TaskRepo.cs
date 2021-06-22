using ApplicationCore.Entities;
using ApplicationCore.RepoInterfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Repo
{
    public class TaskRepo : EfRepo<ETask>, ITaskRepo
    {
        public TaskRepo(TaskManagementDbContext dbContext) : base(dbContext)
        {
        }
    }
}
