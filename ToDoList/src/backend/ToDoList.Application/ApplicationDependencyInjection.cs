using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sqids;
using ToDoList.Application.Services.Mapper;
using ToDoList.Application.UseCases.Login.DoLogin;
using ToDoList.Application.UseCases.TodoTask.Create;
using ToDoList.Application.UseCases.TodoTask.Delete;
using ToDoList.Application.UseCases.TodoTask.GetAllUserTasks;
using ToDoList.Application.UseCases.TodoTask.GetById;
using ToDoList.Application.UseCases.TodoTask.Update;
using ToDoList.Application.UseCases.User.Password.Change;
using ToDoList.Application.UseCases.User.Profile;
using ToDoList.Application.UseCases.User.Register;
using ToDoList.Application.UseCases.User.Update;

namespace ToDoList.Application;

public static class ApplicationDependencyInjection
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddAutoMapper(services);
        AddUseCases(services);
        AddIdEncoder(services, configuration);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddScoped(option => new AutoMapper.MapperConfiguration(autoMapperOptions =>
        {
            var sqids = option.GetService<SqidsEncoder<long>>()!;

            autoMapperOptions.AddProfile(new AutoMapping(sqids));
        }).CreateMapper());
    }

    private static void AddIdEncoder(IServiceCollection services, IConfiguration configuration)
    {
        var sqids = new SqidsEncoder<long>(new()
        {
            Alphabet = configuration.GetValue<string>("Settings:IdEncoder:Alphabet")!,
            MinLength = configuration.GetValue<int>("Settings:IdEncoder:MinLength")
        });

        services.AddSingleton(sqids);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<ILoginUseCase, LoginUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddScoped<IPasswordChangeUseCase, PasswordChangeUseCase>();
        services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
        services.AddScoped<ICreateTodoTaskUseCase, CreateTodoTaskUseCase>();
        services.AddScoped<IUpdateTodoTaskUseCase, UpdateTodoTaskUseCase>();
        services.AddScoped<IGetAllUserTasksUseCase, GetAllUserTasksUseCase>();
        services.AddScoped<IGetTaskById, GetTaskById>();
        services.AddScoped<IDeleteTaskUseCase, DeleteTaskUseCase>();
    }
}