using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using BlackList.Application.Mappings;

namespace BlackList.Api.Extensions;

[ExcludeFromCodeCoverage]
public static class AddAutoMapperConfig
{
    public static IMapper Initialize()
    {
        var config = new MapperConfiguration(c =>
        {
            c.AddProfile(new PlayerMapper());
            c.AddProfile(new UserMapper());
        });

        var mapper = config.CreateMapper();

        return mapper;
    }
}
