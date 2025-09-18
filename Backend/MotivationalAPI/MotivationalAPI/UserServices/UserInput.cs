namespace MotivationalAPI.UserServices;


// Transional class for user;
public class UserInput  
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
	public string Role { get; set; }
	public int Lifepoints { get; set; }
	public int WeedStones { get; set; }

	public UserInput(int id, string name, string email, string password, string role, int lifepoints, int weedStones)
	{
		Id = id;
		Name = name;
		Email = email;
		Password = password;
		Role = role;
		Lifepoints = lifepoints;
		WeedStones = weedStones;
	}
}