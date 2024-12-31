using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using ToDoList.API.Filters;
using ToDoList.API.Token;
using ToDoList.Application;
using ToDoList.Domain.Security.Tokens;
using ToDoList.Infrastructure;
using ToDoList.Infrastructure.Extensions;
using ToDoList.Infrastructure.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
{
    var scheme = "Bearer";

    opts.AddSecurityDefinition(scheme, new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = scheme
    });

    opts.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = scheme
                },
                Scheme = "oauth2",
                Name = scheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<ITokenProvider, HttpContextTokenValue>();

builder.Services.AddRouting(opts => opts.LowercaseUrls = true);

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

MigrateDatabase();

app.Run();

void MigrateDatabase()
{
    var connectionString = builder.Configuration.ConnectionStringBuilder();
    var serviceProvider = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

    DataBaseMigration.Migrate(connectionString, serviceProvider.ServiceProvider);
}
