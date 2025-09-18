using Microsoft.AspNetCore.Identity;

namespace MotivationalAPI.UserServices;

public class User
{
	public int Id { get; set; }
	public string UserName { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
	public string Role { get; set; }
	public int Lifepoints { get; set; }
	public int WeedStones { get; set; }
	
	
	public User() //for Dapper. 
	{
		
	}

	public User(int id, string name, string email, string password, string role, int lifepoints, int weedStones)
	{
		Id = id;
		UserName = name;
		Email = email;
		Password = password;
		Role = role;
		Lifepoints = lifepoints;
		WeedStones = weedStones;
	}
}