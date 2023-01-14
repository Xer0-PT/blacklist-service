namespace BlackList.Api.Extensions;

using AutoMapper;
using BlackList.Application.Mappings;

public static class AddAutoMapperConfig
{
    public static IMapper Initialize()
    {
        var config = new MapperConfiguration(c =>
        {
            c.AddProfile(new BlackListedPlayerMapper());
            c.AddProfile(new UserMapper());
        });

        var mapper = config.CreateMapper();

        return mapper;
    }
}
