using AutoMapper;
using CommonTestUtilities.IdEncryption;
using ToDoList.Application.Services.Mapper;

namespace CommonTestUtilities.AutoMapper;

public class AutoMapperBuilder
{
    public static IMapper Build()
    {
        var sqids = IdEncripterBuilder.Build();

        var mapper = new MapperConfiguration(opts =>
        {
            opts.AddProfile(new AutoMapping(sqids));
        }).CreateMapper();

        return mapper;
    }
}