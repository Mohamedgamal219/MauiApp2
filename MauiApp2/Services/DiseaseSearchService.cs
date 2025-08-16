using MauiApp2.Models;
namespace MauiApp2.Services;
public class DiseaseSearchService
{
    private readonly List<FishDisease> _diseases = new()
    {
        new FishDisease{ Id=1, Name="تعفن الزعانف", Description="تآكل وتهتك بالزعانف", Symptoms="تآكل - احمرار - ضعف", Treatment="تحسين جودة المياه + مضادات بكتيرية" },
        new FishDisease{ Id=2, Name="فطريات الجلد", Description="بقع قطنية بيضاء", Symptoms="بقع بيضاء قطنية - خدش", Treatment="مضاد فطري + ملح بنسب آمنة" },
        new FishDisease{ Id=3, Name="طفيليات خارجية", Description="حكة وفقدان شهية", Symptoms="حكة - سباحة غير طبيعية", Treatment="حمام ملحي + مضاد طفيل" }
    };
    public Task<List<FishDisease>> SearchAsync(string query)
    {
        query = query?.Trim() ?? string.Empty;
        var results = _diseases.Where(d =>
            d.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            d.Description.Contains(query, StringComparison.OrdinalIgnoreCase) ||
            d.Symptoms.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
        return Task.FromResult(results);
    }
}
