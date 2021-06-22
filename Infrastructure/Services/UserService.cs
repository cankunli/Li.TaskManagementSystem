using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;
using ApplicationCore.RepoInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        public UserService(IUserRepo userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }
        public async Task<UserResponseModel> CreateUser(UserRequestModel model)
        {
            var e = new User
            {
                Email = model.Email,
                Password = model.Password,
                Fullname = model.Fullname,
                Mobileno = model.Mobileno
            };
            var result = await _userRepo.AddAsync(e);
            var response = new UserResponseModel
            {
                Email = result.Email,
                Fullname = result.Fullname,
                Mobileno = result.Mobileno
            };
            return response;
        }

        public async Task DeleteUser(int id)
        {
            var e = await _userRepo.GetByIdAsync(id);
            await _userRepo.DeleteAsync(e);
        }

        public async Task<IEnumerable<UserResponseModel>> GetAllUsers()
        {
            var result = await _userRepo.ListAllAsync();
            var userList = new List<UserResponseModel>();

            foreach (var user in result)
            {
                userList.Add(new UserResponseModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Fullname = user.Fullname,
                    Mobileno = user.Mobileno
                });
            }
            return userList;
        }

        public async Task<UserResponseModel> GetUserById(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User", id);
            }
            var tasks = _mapper.Map<IEnumerable<TaskResponseModel>>(user.Tasks);
            var histories = _mapper.Map<IEnumerable<TaskResponseModel>>(user.TasksHistories);
            var userModel = _mapper.Map<UserResponseModel>(user);
            userModel.Tasks = tasks;
            userModel.TasksHistories = histories;
            return userModel;
        }

        public async Task<UserResponseModel> UpdateUser(UserUpdateModel model)
        {
            var i = await _userRepo.GetByIdAsync(model.Id);
            if (i == null)
            {
                throw new NotFoundException("User", model.Id);
            }

            i.Email = model.Email;
            i.Fullname = model.Fullname;
            i.Mobileno = model.Mobileno;

            var res = await _userRepo.UpdateAsync(i);
            var response = new UserResponseModel
            {
                Email = res.Email,
                Fullname = res.Fullname,
                Mobileno = res.Mobileno
            };
            return response;
        }

        public async Task<bool> UserExists(string email)
        {
            return await _userRepo.UserExists(email);
        }
    }
}
