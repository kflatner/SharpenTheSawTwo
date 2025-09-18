using MotivationalAPI.TaskServices;
using Task = System.Threading.Tasks.Task;

namespace MotivationalAPI.UserServices;

public interface IDatabase
{
		public Task AddUser(User user, UserInput userInput);
		public Task<List<User>> GetAllUsers();
		public Task<User> GetUserById(int id);
		public void UpdateUser(UserInput userInput, int id);
		public void DeleteUser(int id);
		
		public Task<User> GetUserLifePoints(int id);
		public Task<User> SetUserLifePoints(int id, UserInput userInput);
		
		public Task<User> GetUserWeedStones(int id);
		public Task<User> SetUserWeedStones(int id, UserInput userInput);
	
		public Task <UserLogin> IsUserExists(UserInput userInput);
	
		public Task<List<TaskServices.Task>>  AddTask ( TaskInput taskInput, int userId);
		public Task<List<TaskServices.Task>> GetAllUsersTasks(int id);
		public Task<User> GetTaskById(int id, UserInput userInput);
		public void UpdateTask(TaskInput taskInput, int id);
		public void DeleteTask(int id);
}