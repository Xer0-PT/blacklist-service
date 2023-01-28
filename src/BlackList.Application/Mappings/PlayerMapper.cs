using AutoMapper;
using BlackList.Application.Dtos;
using BlackList.Domain.Entities;

namespace BlackList.Application.Mappings;

public class PlayerMapper : Profile
{
    public PlayerMapper()
    {
        CreateMap<Player, PlayerDto>();
    }
}
