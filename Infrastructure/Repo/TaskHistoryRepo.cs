using ApplicationCore.Entities;
using ApplicationCore.RepoInterfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repo
{
    public class TaskHistoryRepo : EfRepo<TaskHistory>, ITaskHistoryRepo
    {
        public TaskHistoryRepo(TaskManagementDbContext dbContext) : base(dbContext)
        {
        }
    }
}
