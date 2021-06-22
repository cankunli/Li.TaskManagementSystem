using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IUserService
    {
        Task<UserResponseModel> CreateUser(UserRequestModel model);
        Task<bool> UserExists(string email);
        Task<UserResponseModel> UpdateUser(UserUpdateModel model);
        Task DeleteUser(int id);
        Task<IEnumerable<UserResponseModel>> GetAllUsers();
        Task<UserResponseModel> GetUserById(int id);
    }
}
