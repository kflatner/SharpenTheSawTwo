namespace MotivationalAPI.UserServices;

public class UserLogin
{
	public string NameStatus { get; set; }
	public string PasswordStatus { get; set; }
	public string Message { get; set; }
	
	public int UserId { get; set; }

	public UserLogin()
	{
		
	}

	public UserLogin(string nameStatus, string passwordStatus, string message, int userId)
	{
		NameStatus = nameStatus;
		PasswordStatus = passwordStatus;
		Message = message;
		UserId = userId; 
	}
}