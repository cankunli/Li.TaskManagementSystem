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
    public class TaskService : ITaskService
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepo _taskRepo;
        private readonly ITaskHistoryRepo _taskhistoryRepo;
        public TaskService(ITaskRepo taskRepo, IMapper mapper, ITaskHistoryRepo taskHistoryRepo)
        {
            _taskRepo = taskRepo;
            _mapper = mapper;
            _taskhistoryRepo = taskHistoryRepo;
        }
        public async Task<TaskResponseModel> CompleteTask(int id)
        {
            var task = await _taskRepo.GetByIdAsync(id);
            var historyRequest = _mapper.Map<TaskRequestModel>(task);
            historyRequest.Completed = DateTime.UtcNow.Date;

            var history = _mapper.Map<TaskHistory>(historyRequest);
            var historyCreated = await _taskhistoryRepo.AddAsync(history);
            var response = _mapper.Map<TaskResponseModel>(historyCreated);
            await this.DeleteTask(id);
            return response;
        }

        public async Task<TaskResponseModel> CreateTask(TaskRequestModel model)
        {
            var e = new ETask
            {
                UserId = model.UserId,
                Title = model.Title,
                Description = model.Description,
                DueDate = model.DueDate,
                Priority = model.Priority,
                Remarks = model.Remarks
            };
            var result = await _taskRepo.AddAsync(e);
            var response = new TaskResponseModel
            {
                UserId = result.UserId,
                Title = result.Title,
                Description = result.Description,
                DueDate = result.DueDate,
                Priority = result.Priority,
                Remarks = result.Remarks
            };
            return response;
        }

        public async Task DeleteTask(int taskId)
        {
            var t = await _taskRepo.GetByIdAsync(taskId);
            await _taskRepo.DeleteAsync(t);
        }

        public async Task<TaskResponseModel> GetTaskDetails(int id)
        {
            var c = await _taskRepo.GetByIdAsync(id);
            if (c == null)
            {
                throw new NotFoundException("Task", id);
            }
            var response = new TaskResponseModel
            {
                Id = c.Id,
                UserId = c.UserId,
                Title = c.Title,
                Description = c.Description,
                DueDate = c.DueDate,
                Priority = c.Priority
            };
            return response;
        }

        public async Task<IEnumerable<TaskResponseModel>> GetTasksByUser(int userId)
        {
            var result = await _taskRepo.ListAsync(i => i.UserId == userId);
            var taskList = new List<TaskResponseModel>();

            foreach (var taskHistory in result)
            {
                taskList.Add(new TaskResponseModel
                {
                    Id = taskHistory.Id,
                    UserId = taskHistory.UserId,
                    Title = taskHistory.Title,
                    Description = taskHistory.Description,
                    DueDate = taskHistory.DueDate,
                    Priority = taskHistory.Priority,
                    Remarks = taskHistory.Remarks
                });
            }
            return taskList;
        }

        public async Task<IEnumerable<TaskResponseModel>> ListAllTask()
        {
            var result = await _taskRepo.ListAllAsync();
            var taskList = new List<TaskResponseModel>();

            foreach (var taskHistory in result)
            {
                taskList.Add(new TaskResponseModel
                {
                    Id = taskHistory.Id,
                    UserId = taskHistory.UserId,
                    Title = taskHistory.Title,
                    Description = taskHistory.Description,
                    DueDate = taskHistory.DueDate,
                    Priority = taskHistory.Priority,
                    Remarks = taskHistory.Remarks
                });
            }
            return taskList;
        }

        public async Task<TaskResponseModel> UpdateTask(TaskRequestModel model)
        {
            var task = _mapper.Map<ETask>(model);
            var updatedTask = await _taskRepo.UpdateAsync(task,
                t => t.Title,
                t => t.Description,
                t => t.DueDate,
                t => t.Priority,
                t => t.Remarks);
            var response = _mapper.Map<TaskResponseModel>(updatedTask);
            return response;
            //var i = await _taskRepo.GetByIdAsync(model.Id);
            //if (i == null)
            //{
            //    throw new NotFoundException("Task", model.Id);
            //}

            //i.UserId = model.UserId;
            //i.Title = model.Title;
            //i.Description = model.Description;
            //i.DueDate = model.DueDate;
            //i.Priority = model.Priority;
            //i.Remarks = model.Remarks;

            //var res = await _taskRepo.UpdateAsync(i);
            //var response = new TaskResponseModel
            //{
            //    Id = res.Id,
            //    UserId = res.UserId,
            //    Title = res.Title,
            //    Description = res.Description,
            //    DueDate = res.DueDate,
            //    Priority = res.Priority,
            //    Remarks = res.Remarks
            //};
            //return response;
        }
    }
}
