using MauiApp2.Models;
namespace MauiApp2.Services;
public class NoteService
{
    private readonly DatabaseService _db;
    public NoteService(DatabaseService db) => _db = db;

    public async Task<List<Note>> GetByPondAsync(Guid pondId)
    {
        var d = await _db.GetAsync();
        return await d.Table<Note>().Where(n=>n.PondId==pondId).OrderByDescending(n=>n.Date).ToListAsync();
    }
    public async Task AddAsync(Note note)
    {
        var d = await _db.GetAsync();
        await d.InsertAsync(note);
    }
    public async Task UpdateAsync(Note note)
    {
        var d = await _db.GetAsync();
        await d.UpdateAsync(note);
    }
    public async Task DeleteAsync(Guid id)
    {
        var d = await _db.GetAsync();
        await d.DeleteAsync<Note>(id);
    }
}
