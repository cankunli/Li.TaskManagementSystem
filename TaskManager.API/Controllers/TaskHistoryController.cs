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
    public class TaskHistoryController : ControllerBase
    {
        private readonly ITaskHistoryService _historyService;

        public TaskHistoryController(ITaskHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> ListAllHistories()
        {
            var histories = await _historyService.ListAllTaskHistories();
            return Ok(histories);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetHistoryById(int id)
        {
            var history = await _historyService.GetHistoryDetails(id);
            return Ok(history);
        }

        [HttpGet]
        [Route("user/{id:int}")]
        public async Task<IActionResult> GetHistoriesByUser(int id)
        {
            var tasks = await _historyService.GetHistoriesByUserID(id);
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHistory(TaskRequestModel taskRequest)
        {
            var createdHistory = await _historyService.CreateTaskHistory(taskRequest);
            return Ok(createdHistory);
        }

        [HttpGet]
        [Route("revert/{id:int}")]
        public async Task<IActionResult> RevertHistory(int id)
        {
            var updateTask = await _historyService.UpdateTaskHistory(id);
            return Ok(updateTask);
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _historyService.DeleteTaskHistory(id);
            return Ok();
        }
    }
}
