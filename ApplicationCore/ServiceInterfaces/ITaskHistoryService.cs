using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface ITaskHistoryService
    {
        Task<TaskResponseModel> CreateTaskHistory(TaskRequestModel model);
        Task<TaskResponseModel> UpdateTaskHistory(int id, char priority = 'E');
        Task DeleteTaskHistory(int id);
        Task<IEnumerable<TaskResponseModel>> ListAllTaskHistories();
        Task<TaskResponseModel> GetHistoryDetails(int id);
        Task<IEnumerable<TaskResponseModel>> GetHistoriesByUserID(int userId);

    }
}
