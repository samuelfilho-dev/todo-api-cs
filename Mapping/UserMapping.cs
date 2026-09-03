using AutoMapper;
using TodoCs.Dtos;
using TodoCs.Models;

namespace TodoCs.Mapping;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<User, UserResponse>();
        CreateMap<CreateUserRequest, User>();
        CreateMap<UpdateUserRequest, User>();
    }
}