using SQLite;
namespace MauiApp2.Models;
[Table("Ponds")]
public class Pond
{
    [PrimaryKey] public Guid Id { get; set; } = Guid.NewGuid();
    [Indexed] public Guid FarmId { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Area { get; set; }
    public double Depth { get; set; }
    public string WaterType { get; set; } = "عذبة";
    public int FishCount { get; set; }
}
