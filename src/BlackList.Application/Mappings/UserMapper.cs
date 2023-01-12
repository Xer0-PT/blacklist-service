namespace BlackList.Application.Mappings;

using AutoMapper;
using BlackList.Application.Dtos;
using BlackList.Domain.Entities;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, UserDto>();
    }
}
