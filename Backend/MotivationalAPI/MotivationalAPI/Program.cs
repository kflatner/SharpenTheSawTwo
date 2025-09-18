using MotivationalAPI.TaskServices;
using MotivationalAPI.TestDB;
using MotivationalAPI.UserServices;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(o => o.AddPolicy("frontend",
	p => p.WithOrigins("*")
		.AllowAnyHeader()
		.AllowAnyMethod()
));



//https://learn.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-9.0

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddScoped<IDatabase, PgsDatabase>(); // asking for IDatabase - giving TestsDb.
builder.Services.AddScoped<UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseCors("frontend");
app.UseHttpsRedirection();

app.MapGet("/api/getUsers",  (UserService userService) => { return userService.GetAllUsers(); })
	.WithName("GetAllUsers");

app.MapGet("/api/getUsersLifePoints/{id}", async (UserService userService, int id) =>
{
	var userPoints = await userService.GetUserLifePoints(id);
	return new
	{ Id = userPoints.Id,
	  Name = userPoints.UserName,
	  Lifepoints = userPoints.Lifepoints };
}).WithName("GetUserLifePoints").WithOpenApi();

app.MapPost("/api/setUsersLifePoints/{id}", async (UserService userService, int id, UserInput userInput) =>
{
	try
	{
		var updatedUser = await userService.SetUserLifePoints(id, userInput);
		return Results.Ok(new
		{ Id = id, Name = updatedUser.UserName, Lifepoints = updatedUser.Lifepoints });
	}

	catch (Exception ex)
	{
		return Results.Problem(ex.StackTrace);
	}
}).WithName("SetUserLifePoints").WithOpenApi();

app.MapGet("/api/getUsersWeedstones/{id}", async (UserService userService, int id) =>
{
	var userPoints = await userService.GetUserWeedStones(id);
	return new
	{ Id = userPoints.Id,
	  Name = userPoints.UserName,
	  Weedstones = userPoints.WeedStones };
}).WithName("GetUserWeedStones").WithOpenApi();


app.MapPost("/api/setUsersWeedstones/{id}", async (UserService userService, int id, UserInput userInput) =>
{
	try
	{
		var updatedUser = await userService.SetUserWeedStones(id, userInput);
		return Results.Ok(new
		{ Id = id, Name = updatedUser.UserName, Weedstones = updatedUser.WeedStones });
	}
	catch (Exception ex)
	{
		return Results.Problem(ex.StackTrace);
	}
}).WithName("SetUserWeedstones").WithOpenApi();

app.MapGet("/api/getTasks/{id}",
		async (UserService userService, int id) => { return await userService.GetAllUsersTasks(id); })
	.WithName("GetAllUsersTasks");


app.MapPost("/api/setNewTask/{userId}", async (UserService userService, int userId, TaskInput taskInput) =>
{
	try
	{
		var newTask = await userService.AddTask(taskInput, userId);
		return Results.Ok( newTask);
	}
	catch (Exception ex)
	{
		return Results.Problem(ex.StackTrace);
	}
}).WithName("SetNewTask").WithOpenApi();


app.MapPost("/api/updateTask/{taskId}", async (UserService userService, int taskId, TaskInput taskInput) =>
{
	try
	{   userService.UpdateTask(taskInput, taskId);
		return Results.Ok($"Task №{taskId} was successfully updated");
	}
	catch (Exception ex)
	{
		return Results.Problem(ex.StackTrace);
	}
}).WithName("UpdateTask").WithOpenApi();


app.MapGet(
	"/api/deleteTask/{taskId}", async (UserService userService, int taskId) =>
	{
		userService.DeleteTask(taskId);
		return Results.Ok($"Task № {taskId} was successfully deleted");
	}).WithName("DeleteBookById").WithOpenApi();

app.MapPost("/api/login", async (UserService userService, UserInput userInput) =>
{
	try
	{   var login =  await userService.IsUserExists(userInput);
		return Results.Ok(login);
	}
	catch (Exception ex)
	{
		return Results.Problem(ex.StackTrace);
	}
}).WithName("Login").WithOpenApi();


app.Run();