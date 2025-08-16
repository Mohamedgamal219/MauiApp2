using SQLite;
namespace MauiApp2.Models;
[Table("Users")]
public class User
{
    [PrimaryKey, AutoIncrement] public int Id { get; set; }
    [Unique] public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Viewer;
}
