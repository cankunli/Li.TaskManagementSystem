using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationCore.RepoInterfaces
{
    public interface ITaskRepo : IAsyncRepo<ETask>
    {
    }
}
