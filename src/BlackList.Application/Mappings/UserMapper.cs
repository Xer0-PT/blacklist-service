using AutoMapper;
using BlackList.Application.Dtos;
using BlackList.Domain.Entities;

namespace BlackList.Application.Mappings;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, UserDto>();
    }
}
