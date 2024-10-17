namespace TouristApp.Blazor.Models;

public class User {
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string? Bio { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
}