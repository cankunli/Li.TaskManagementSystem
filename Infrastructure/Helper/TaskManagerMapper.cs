using ApplicationCore.Entities;
using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helper
{
    public class TaskManagerMapper : Profile
    {
        public TaskManagerMapper()
        {
            CreateMap<User, UserResponseModel>()
                .ForMember(x => x.Tasks, opt => opt.Ignore())
                .ForMember(x => x.TasksHistories, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<UserRequestModel, User>();
            CreateMap<UserUpdateModel, User>();

            CreateMap<ETask, TaskResponseModel>()
                .ForMember(x => x.User, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<TaskRequestModel, ETask>().ReverseMap();

            CreateMap<TaskHistory, TaskResponseModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(m => m.TaskId))
                .ForMember(x => x.User, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<TaskRequestModel, TaskHistory>()
                .ForMember(x => x.TaskId, opt => opt.MapFrom(m => m.Id))
                .ReverseMap();
        }
    }
}
