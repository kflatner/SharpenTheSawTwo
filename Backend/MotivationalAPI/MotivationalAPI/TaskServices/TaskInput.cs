namespace MotivationalAPI.TaskServices;

public class TaskInput
{
	public int Id { get; set; }
	public string Title { get; set; }
	public bool Status { get; set; }

	public TaskInput(int id, string title, bool status)
	{
		Id = id;
		Title = title;
		Status = status;
	}
}