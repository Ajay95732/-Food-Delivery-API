namespace FoodDelivery.API.Models;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public string Email { get; set; } = "";

    public string PasswordHash { get; set; } = "";

    // New Property
    public string Role { get; set; } = "User";
}