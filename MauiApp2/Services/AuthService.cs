using MauiApp2.Models;
namespace MauiApp2.Services;
public class AuthService
{
    private readonly DatabaseService _dbService;
    public AuthService(DatabaseService db) => _dbService = db;
    public User? CurrentUser { get; private set; }
    public async Task<User?> LoginAsync(string username, string password)
    {
        var db = await _dbService.GetAsync();
        var user = await db.Table<User>().Where(u => u.Username == username && u.Password == password).FirstOrDefaultAsync();
        CurrentUser = user;
        return user;
    }
    public void Logout() => CurrentUser = null;
}
