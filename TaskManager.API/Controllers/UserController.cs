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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserRequestModel model)
        {
            if (await _userService.UserExists(model.Email)) return BadRequest("Email is taken");
            var users = await _userService.CreateUser(model);
            return Ok(users);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserUpdateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please check the info you entered");
            }
            var users = await _userService.UpdateUser(model);
            return Ok(users);
        }

        [HttpDelete]
        [Route("{intId:int}")]
        public async Task<IActionResult> DeleteUser(int intId)
        {
            await _userService.DeleteUser(intId);
            return Ok();
        }


        [HttpGet]
        [Route("")]
        public async Task<IActionResult> ListAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            return Ok(user);
        }
    }
}
