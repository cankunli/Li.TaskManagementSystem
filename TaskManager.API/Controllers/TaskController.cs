using ApplicationCore.Models.Request;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> ListAllTasks()
        {
            var tasks = await _taskService.ListAllTask();
            return Ok(tasks);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskDetails(id);
            return Ok(task);
        }

        [HttpGet]
        [Route("user/{id:int}")]
        public async Task<IActionResult> GetTasksByUser(int id)
        {
            var tasks = await _taskService.GetTasksByUser(id);
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskRequestModel model)
        {
            var createdTask = await _taskService.CreateTask(model);
            return Ok(createdTask);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTask(TaskRequestModel model)
        {
            var updatedTask = await _taskService.UpdateTask(model);
            return Ok(updatedTask);
        }

        [HttpDelete]
        [Route("{intId:int}")]
        public async Task<IActionResult> DeleteTask(int intId)
        {
            await _taskService.DeleteTask(intId);
            return Ok();
        }

        [HttpGet]
        [Route("complete/{id:int}")]
        public async Task<IActionResult> CompleteTask(int id)
        {
            var complete = await _taskService.CompleteTask(id);
            return Ok(complete);
        }
    }
}
