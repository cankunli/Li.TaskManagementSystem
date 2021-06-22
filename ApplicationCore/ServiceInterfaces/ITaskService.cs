using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface ITaskService
    {
        Task<TaskResponseModel> CreateTask(TaskRequestModel model);
        Task<TaskResponseModel> UpdateTask(TaskRequestModel model);
        Task DeleteTask(int taskId);

        Task<IEnumerable<TaskResponseModel>> ListAllTask();
        Task<TaskResponseModel> GetTaskDetails(int id);
        Task<TaskResponseModel> CompleteTask(int id);
        Task<IEnumerable<TaskResponseModel>> GetTasksByUser(int userId);
    }
}
