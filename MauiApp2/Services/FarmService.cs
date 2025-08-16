using MauiApp2.Models;
namespace MauiApp2.Services;
public class FarmService
{
    private readonly DatabaseService _db;
    public FarmService(DatabaseService db) => _db = db;
    public async Task<List<Farm>> GetAllAsync()
    {
        var d = await _db.GetAsync();
        return await d.Table<Farm>().OrderBy(f=>f.Name).ToListAsync();
    }
    public async Task AddAsync(Farm farm)
    {
        var d = await _db.GetAsync();
        await d.InsertAsync(farm);
    }
    public async Task UpdateAsync(Farm farm)
    {
        var d = await _db.GetAsync();
        await d.UpdateAsync(farm);
    }
    public async Task DeleteAsync(Guid id)
    {
        var d = await _db.GetAsync();
        await d.DeleteAsync<Farm>(id);
    }
}
