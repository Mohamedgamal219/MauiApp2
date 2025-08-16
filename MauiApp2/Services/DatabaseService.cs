using SQLite;
using MauiApp2.Models;

namespace MauiApp2.Services;
public class DatabaseService
{
    private SQLiteAsyncConnection? _db;
    public async Task<SQLiteAsyncConnection> GetAsync()
    {
        if (_db != null) return _db;
        var path = Path.Combine(FileSystem.AppDataDirectory, "fishfarm.db3");
        _db = new SQLiteAsyncConnection(path);
        await _db.CreateTableAsync<User>();
        await _db.CreateTableAsync<Farm>();
        await _db.CreateTableAsync<Pond>();
        await _db.CreateTableAsync<Note>();
        if (await _db.Table<User>().CountAsync() == 0)
        {
            await _db.InsertAllAsync(new[] {
                new User { Username="admin", Password="1234", Role=UserRole.Admin },
                new User { Username="manager", Password="1234", Role=UserRole.Manager },
                new User { Username="engineer", Password="1234", Role=UserRole.Engineer },
                new User { Username="viewer", Password="1234", Role=UserRole.Viewer }
            });
        }
        return _db;
    }
}
