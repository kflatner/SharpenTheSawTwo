using MotivationalAPI.TaskServices;
using Npgsql;
using Dapper;
using Microsoft.VisualBasic.CompilerServices;
using Task = System.Threading.Tasks.Task;

namespace MotivationalAPI.UserServices;

public class PgsDatabase : IDatabase
{
	private const string DATABASE_NAME = "sharpenDB";


	private const string CONNECTION_STRING =
		$"Host=sharpenthesawsql.postgres.database.azure.com;Port=5432;Username=sharpenthesaw;Password=neprash1!;Database={DATABASE_NAME};SearchPath=public;";

	private NpgsqlConnection _connection;

	public Task AddUser(User user, UserInput userInput)
	{
		throw new NotImplementedException();
	}

	public async Task<List<User>> GetAllUsers()
	{
		_connection = new NpgsqlConnection(CONNECTION_STRING);
		Console.WriteLine(CONNECTION_STRING);
		_connection.Open();
		string commandText =
			$"SELECT id   AS Id," +
			$"\n   username AS Username," +
			$"     email AS Email," +
			$"     password_hash AS Password," +
			$"\n   role AS Role," +
			$"\n   lifepoint AS Lifepoints," +
			$"\n   weedstones AS Weedstones" +
			$"\n FROM public.users  " +
			$"ORDER BY id";
		var users = _connection.Query<User>(commandText);
		await _connection.CloseAsync();
		return users.ToList();
	}

	public Task<User> GetUserById(int id)
	{
		throw new NotImplementedException();
	}

	public void UpdateUser(UserInput userInput, int id)
	{
		throw new NotImplementedException();
	}

	public void DeleteUser(int id)
	{
		throw new NotImplementedException();
	}

	public async Task<User> GetUserLifePoints(int id)
	{
		_connection = new NpgsqlConnection(CONNECTION_STRING);
		_connection.Open();
		string commandText =
			$"SELECT id   AS Id," +
			$"\n   username AS Username," +
			$"     email AS Email," +
			$"     password_hash AS Password," +
			$"\n   role AS Role," +
			$"\n   lifepoint AS Lifepoints," +
			$"\n   weedstones AS Weedstones" +
			$"\n FROM public.users  " +
			$"\n WHERE id = @Id";
		var points = await _connection.QuerySingleOrDefaultAsync<User>(commandText, new
		{ Id = id });
		await _connection.CloseAsync();
		return points;
	}

	public async Task<User> SetUserLifePoints(int id, UserInput userInput)
	{
		_connection = new NpgsqlConnection(CONNECTION_STRING);
		_connection.Open();
		Console.WriteLine(userInput.Lifepoints);
		string commandText =
			$" UPDATE users  SET  " +
			$"\n   lifepoint = @Lifepoints" +
			$"\n WHERE id = @Id";
		var queryArgs = new
		{ Id = id, Lifepoints = userInput.Lifepoints };
		await _connection.ExecuteAsync(commandText, queryArgs);
		await _connection.CloseAsync();
		return await GetUserLifePoints(id);
	}

	public async Task<User> GetUserWeedStones(int id)
	{
		_connection = new NpgsqlConnection(CONNECTION_STRING);
		_connection.Open();
		string commandText =
			$"SELECT id   AS Id," +
			$"\n   username AS Username," +
			$"     email AS Email," +
			$"     password_hash AS Password," +
			$"\n   role AS Role," +
			$"\n   lifepoint AS Lifepoints," +
			$"\n   weedstones AS Weedstones" +
			$"\n FROM public.users  " +
			$"\n WHERE id = @Id";
		var points = await _connection.QuerySingleOrDefaultAsync<User>(commandText, new
		{ Id = id });
		await _connection.CloseAsync();
		return points;
	}

	public async Task<User> SetUserWeedStones(int id, UserInput userInput)
	{
		_connection = new NpgsqlConnection(CONNECTION_STRING);
		_connection.Open();
		string commandText =
			$" UPDATE users  SET  " +
			$"\n   weedstones = @Weedstones" +
			$"\n WHERE id = @Id";
		var queryArgs = new
		{ Id = id, Weedstones = userInput.WeedStones };
		await _connection.ExecuteAsync(commandText, queryArgs);
		await _connection.CloseAsync();
		return await GetUserWeedStones(id);
	}

	public async Task<UserLogin> IsUserExists(UserInput userInput)
	{
		_connection = new NpgsqlConnection(CONNECTION_STRING);
		_connection.Open();
		string commandText =
			$"WITH name_status as (\n\t\t\tSELECT * FROM users\n\t\tWHERE username = @Username\n\t\t\t), \n\t\tpassword_status AS (\n\t\t\tSELECT * FROM users\n\t\tWHERE username = @Username AND password_hash = @Password\n\t\t\t),\n\t\tuser_id AS (\n\t\t\tSELECT id FROM users\n\t\tWHERE username = @Username AND password_hash = @Password\n\t\t\t), \n\n\t\tcalc AS ( SELECT \n\t\t(SELECT COUNT (*) FROM name_status)  AS nameStatus,\n\t\t(SELECT COUNT (*) FROM password_status) AS passwordStatus, \n\t\t(SELECT id AS userId FROM user_id), \n\t\t'' AS message\n\t\t\t)\n\n\t\tSELECT namestatus, passwordstatus, userId, \n\t\t\tCASE \n\t\tWHEN namestatus = 0 THEN 'No user found'\n\t\tWHEN passwordstatus = 0 THEN 'Wrong password'\n\t\tWHEN namestatus = 1 AND passwordstatus = 1 THEN 'Login successful'\n\t\tEND as message\n\t\tFROM calc";
		Console.WriteLine(commandText);
		var userLogin = await _connection.QuerySingleOrDefaultAsync<UserLogin>(commandText, new
		{ Username = userInput.Name, Password = userInput.Password });
		await _connection.CloseAsync();
		return userLogin;
	}
	

	private async Task<string> IsPasswordGood(string name, string password)
	{
		_connection = new NpgsqlConnection(CONNECTION_STRING);
		_connection.Open();
		string commandText =
			$"WITH t as (\n SELECT * FROM users \n WHERE username = @Name AND password_hash = @Password \n )\n SELECT \n COUNT (*) FROM t AS count";

		var intStatus = await _connection.QuerySingleOrDefaultAsync<int>(commandText, new
		{ Name = name, Password = password });
		await _connection.CloseAsync();
		return intStatus == 1 ? "Password - OK" : "Password doesn't match";
	}

	public async Task<List<TaskServices.Task>> AddTask(TaskInput taskInput, int userId)
	{
		_connection = new NpgsqlConnection(CONNECTION_STRING);
		_connection.Open();
		string commandText =
			$"WITH id_t AS \n (INSERT INTO tasks ( title) \n VALUES (@Title)\n RETURNING id \n ) \n INSERT INTO user_tasks (user_id, task_id, status) \n SELECT @UserId, id, false FROM  id_t";

		var queryArgs = new
		{ Title = taskInput.Title, UserId = userId };
		await _connection.ExecuteAsync(commandText, queryArgs);
		await _connection.CloseAsync();
		return await GetAllUsersTasks(userId);
	}
	

	public async Task<List<MotivationalAPI.TaskServices.Task>> GetAllUsersTasks(int id)
	{
		await using var _connection = new NpgsqlConnection(CONNECTION_STRING);
		Console.WriteLine(CONNECTION_STRING);
		await _connection.OpenAsync();
		string commandText =
			$"SELECT f.task_id AS id, z.title,  f.status FROM user_tasks f JOIN tasks z ON f.task_id = z.id \n WHERE f.user_id = @UserId";
		var tasks = await _connection.QueryAsync<TaskServices.Task>(commandText, new
		{ UserId = id });
		await _connection.CloseAsync();
		return tasks.ToList();
	}


	public Task<User> GetTaskById(int id, UserInput userInput)
	{
		throw new NotImplementedException();
	}

	public async void UpdateTask(TaskInput taskInput, int id)
	{
		try
		{
			await using var _connection = new NpgsqlConnection(CONNECTION_STRING);
			Console.WriteLine(CONNECTION_STRING);
			await _connection.OpenAsync();
			if (taskInput.Title != "") // In case if we just need to change text in task. 
			{
				string commandText =
					$"UPDATE tasks  SET  " +
					$"\n   title = @Title" +
					$"\n WHERE id = @Id";
				await _connection.ExecuteAsync(commandText, new
				{ Title = taskInput.Title, Id = id });
				await _connection.CloseAsync();
			}

			else
			{
				string commandText =
					$"UPDATE user_tasks  SET  " +
					$"\n   status = @Status" +
					$"\n WHERE task_id = @Id";
				await _connection.ExecuteAsync(commandText, new
				{ Id = id, Status = taskInput.Status });
				await _connection.CloseAsync();
			}
		}
		catch (NpgsqlException e)
		{
			Console.WriteLine(e.Message);
		}
	}

	public Task<User> GetTaskById(int id)
	{
		throw new NotImplementedException();
	}


	public async void DeleteTask(int id)
	{
		try
		{
			await using var _connection = new NpgsqlConnection(CONNECTION_STRING);
			await _connection.OpenAsync();
			string commandText =
				$"DELETE FROM user_tasks WHERE task_id  = @Id; " +
				$" \n DELETE FROM tasks WHERE id = @Id;";
			await _connection.ExecuteAsync(commandText, new
			{ Id = id });
			await _connection.CloseAsync();
		}
		catch (NpgsqlException e)
		{
			Console.WriteLine(e.Message);
		}
	}
}