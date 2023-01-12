namespace BlackList.Application.Mappings;

using AutoMapper;
using BlackList.Application.Dtos;
using BlackList.Domain.Entities;

public class BlackListedPlayerMapper : Profile
{
    public BlackListedPlayerMapper()
    {
        CreateMap<BlackListedPlayer, BlackListedPlayerDto>();
    }
}
