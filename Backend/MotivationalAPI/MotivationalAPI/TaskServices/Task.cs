namespace MotivationalAPI.TaskServices;

public class Task
{
	public int Id { get; set; }
	public string Title { get; set; }
	public bool Status { get; set; }
	
	public Task() //for Dapper
	{
		
	}

	public Task(int id, string title, bool status)
	{
		Id = id;
		Title = title;
		Status = status;
	}
}