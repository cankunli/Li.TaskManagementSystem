using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;
using ApplicationCore.RepoInterfaces;
using ApplicationCore.ServiceInterfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class TaskHistoryService : ITaskHistoryService
    {
        private readonly ITaskHistoryRepo _taskHistoryRepo;
        private readonly IMapper _mapper;
        private readonly ITaskService _taskService;
        public TaskHistoryService(ITaskHistoryRepo taskRepo, IMapper mapper, ITaskService taskService)
        {
            _taskHistoryRepo = taskRepo;
            _mapper = mapper;
            _taskService = taskService;
        }
        public async Task<TaskResponseModel> CreateTaskHistory(TaskRequestModel model)
        {
            var i = new TaskHistory
            {
                UserId = model.UserId,
                Title = model.Title,
                Description = model.Description,
                DueDate = model.DueDate,
                Completed = model.Completed,
                Remarks = model.Remarks
            };
            var res = await _taskHistoryRepo.AddAsync(i);
            var response = new TaskResponseModel
            {
                Id = res.TaskId,
                UserId = res.UserId,
                Title = res.Title,
                Description = res.Description,
                DueDate = res.DueDate,
                Completed = res.Completed,
                Remarks = res.Remarks
            };
            return response;
        }

        public async Task DeleteTaskHistory(int id)
        {
            var t = await _taskHistoryRepo.GetByIdAsync(id);
            await _taskHistoryRepo.DeleteAsync(t);
        }

        public async Task<IEnumerable<TaskResponseModel>> GetHistoriesByUserID(int userId)
        {
            var result = await _taskHistoryRepo.ListAsync(i => i.UserId == userId);
            var taskHistoryList = new List<TaskResponseModel>();

            foreach (var taskHistory in result)
            {
                taskHistoryList.Add(new TaskResponseModel
                {
                    Id = taskHistory.TaskId,
                    UserId = taskHistory.UserId,
                    Title = taskHistory.Title,
                    Description = taskHistory.Description,
                    DueDate = taskHistory.DueDate,
                    Completed = taskHistory.Completed,
                    Remarks = taskHistory.Remarks
                });
            }
            return taskHistoryList;
        }

        public async Task<TaskResponseModel> GetHistoryDetails(int id)
        {
            var inter = await _taskHistoryRepo.GetByIdAsync(id);
            if (inter == null)
            {
                throw new NotFoundException("Task History", id);
            }
            var resModel = new TaskResponseModel
            {
                Id = inter.TaskId,
                UserId = inter.UserId,
                Title = inter.Title,
                Description = inter.Description,
                DueDate = inter.DueDate,
                Completed = inter.Completed,
                Remarks = inter.Remarks
            };
            return resModel;
        }

        public async Task<IEnumerable<TaskResponseModel>> ListAllTaskHistories()
        {
            var result = await _taskHistoryRepo.ListAllAsync();
            var taskHistoryList = new List<TaskResponseModel>();

            foreach (var taskHistory in result)
            {
                taskHistoryList.Add(new TaskResponseModel
                {
                    Id = taskHistory.TaskId,
                    UserId = taskHistory.UserId,
                    Title = taskHistory.Title,
                    Description = taskHistory.Description,
                    DueDate = taskHistory.DueDate,
                    Completed = taskHistory.Completed,
                    Remarks = taskHistory.Remarks
                });
            }
            return taskHistoryList;
        }

        public async Task<TaskResponseModel> UpdateTaskHistory(int id, char priority = 'E')
        {
            var history = await _taskHistoryRepo.GetByIdAsync(id);
            var taskRequest = _mapper.Map<TaskRequestModel>(history);
            taskRequest.Id = null;
            taskRequest.Priority = priority;
            var response = await _taskService.CreateTask(taskRequest);
            await this.DeleteTaskHistory(id);
            return response;
        }
    }
}
