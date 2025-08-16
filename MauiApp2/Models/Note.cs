using SQLite;
namespace MauiApp2.Models;
[Table("Notes")]
public class Note
{
    [PrimaryKey] public Guid Id { get; set; } = Guid.NewGuid();
    [Indexed] public Guid PondId { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public double Temperature { get; set; }
    public string Feeding { get; set; } = string.Empty;
    public int Mortality { get; set; }
    public string Comment { get; set; } = string.Empty;
    public double PH { get; set; }
    public double DissolvedOxygen { get; set; }
    public double Ammonia { get; set; }
    public double Salinity { get; set; }
}
