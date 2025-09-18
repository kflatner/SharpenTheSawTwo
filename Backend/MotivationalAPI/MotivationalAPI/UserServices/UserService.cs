using MotivationalAPI.TaskServices;
using Task = System.Threading.Tasks.Task;

namespace MotivationalAPI.UserServices;

public class UserService(IDatabase repository)
{
	private IDatabase _repository = repository;

	public Task AddUser(User user, UserInput userInput)
	{
		return _repository.AddUser(user, userInput);
	}

	public Task<List<User>> GetAllUsers()
	{
		return  _repository.GetAllUsers();
	}

	public Task<User> GetUserById(int id)
	{
		return _repository.GetUserById(id);
	}

	public Task<UserLogin> IsUserExists(UserInput userInput)
	{
		return _repository.IsUserExists(userInput);
	}


	public Task<User> GetUserLifePoints(int id)
	{
		return _repository.GetUserLifePoints(id);
	} 
	public Task<User> SetUserLifePoints(int id, UserInput userInput)
	{
		return _repository.SetUserLifePoints(id, userInput);
	}

	public Task<User> GetUserWeedStones(int id)
	{
		return _repository.GetUserWeedStones(id);
	}
	
	public Task<User> SetUserWeedStones(int id, UserInput userInput)
	{
		return _repository.SetUserWeedStones(id, userInput);
	}

	public void DeleteUser(int id)
	{
		_repository.DeleteUser(id);
	}

	public void UpdateUser(UserInput userInput, int id)
	{
		_repository.UpdateUser(userInput, id);
	}
	//
	public Task <List<TaskServices.Task>> AddTask( TaskInput taskInput, int UserId)
	{
		return _repository.AddTask(taskInput, UserId);
	}

	public Task<List<TaskServices.Task>> GetAllUsersTasks(int UserId)
	{
		return _repository.GetAllUsersTasks(UserId);
	}

	public Task<User> GetTaskById(int id, UserInput userInput)
	{
		return _repository.GetTaskById(id, userInput);
	}

	public void DeleteTask(int id)
	{
		_repository.DeleteTask(id);
	}

	public void UpdateTask(TaskInput taskInput, int id)
	{
		_repository.UpdateTask(taskInput, id);
	}
}