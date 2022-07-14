using AutoMapper;
using Data;
using Dtos;
using Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Setup connection to our SQL server
var sqlConnectionBuilder = new SqlConnectionStringBuilder();

sqlConnectionBuilder.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection"); // To run locally, use "DefaultConnection": "Server=tcp:localhost,1433;Initial Catalog=CommandDb;"
sqlConnectionBuilder.UserID = builder.Configuration["UserId"];
sqlConnectionBuilder.Password = builder.Configuration["Password"];

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(sqlConnectionBuilder.ConnectionString));
builder.Services.AddScoped<ICommandRepo, CommandRepo>(); // Dependency injection
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // Register automap for dependency injection

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Note: in a fully-fledged MVC, these would have their own dedicated controller class
app.MapGet("api/v1/commands", async (ICommandRepo repo, IMapper mapper) =>
{
    var commands = await repo.GetAllCommands();
    return Results.Ok(mapper.Map<IEnumerable<CommandReadDto>>(commands));
});

app.MapGet("api/v1/commands/{id}", async (ICommandRepo repo, IMapper mapper, [FromRoute] int id) =>
{
    var command = await repo.GetCommandById(id);
    if (command != null) return Results.Ok(mapper.Map<CommandReadDto>(command));
    return Results.NotFound();
});

app.MapPost("api/v1/commands", async (ICommandRepo repo, IMapper mapper, CommandCreateDto cmdCreateDto) =>
{
    var commandModel = mapper.Map<Command>(cmdCreateDto);

    await repo.CreateCommand(commandModel);
    await repo.SaveChangesAsync();

    var cmdReadDto = mapper.Map<CommandReadDto>(commandModel);

    return Results.Created($"api/v1/commands/{cmdReadDto.Id}", cmdReadDto);
});

app.MapPut("api/v1/commands", async (ICommandRepo repo, IMapper mapper, int id, CommandUpdateDto cmdUpdateDto) =>
{
    var command = await repo.GetCommandById(id);
    if (command == null) return Results.NotFound();
    mapper.Map(cmdUpdateDto, command);

    await repo.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("api/v1/commands/{id}", async (ICommandRepo repo, IMapper mapper, int id) =>
{
    var command = await repo.GetCommandById(id);
    if (command == null) return Results.NotFound();
    repo.DeleteCommand(command);

    await repo.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();