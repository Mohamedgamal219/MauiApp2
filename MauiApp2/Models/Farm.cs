using SQLite;
namespace MauiApp2.Models;
[Table("Farms")]
public class Farm
{
    [PrimaryKey] public Guid Id { get; set; } = Guid.NewGuid();
    [Indexed] public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
}
