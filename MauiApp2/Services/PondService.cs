using MauiApp2.Models;
namespace MauiApp2.Services;
public class PondService
{
    private readonly DatabaseService _db;
    public PondService(DatabaseService db) => _db = db;

    public async Task<List<Pond>> GetByFarmAsync(Guid farmId)
    {
        var d = await _db.GetAsync();
        return await d.Table<Pond>().Where(p=>p.FarmId==farmId).OrderBy(p=>p.Name).ToListAsync();
    }
    public async Task AddAsync(Pond pond)
    {
        var d = await _db.GetAsync();
        await d.InsertAsync(pond);
    }
    public async Task UpdateAsync(Pond pond)
    {
        var d = await _db.GetAsync();
        await d.UpdateAsync(pond);
    }
    public async Task DeleteAsync(Guid id)
    {
        var d = await _db.GetAsync();
        await d.DeleteAsync<Pond>(id);
    }
}
