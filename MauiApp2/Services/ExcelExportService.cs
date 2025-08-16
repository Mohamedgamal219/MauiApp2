using ClosedXML.Excel;
using MauiApp2.Models;
namespace MauiApp2.Services;
public class ExcelExportService
{
    public async Task<string> ExportPondAsync(Farm farm, Pond pond, List<Note> notes)
    {
        var fileName = $"{Sanitize(farm.Name)}_{Sanitize(pond.Name)}_{DateTime.Now:yyyyMMdd_HHmm}.xlsx";
        var exportDir = FileSystem.Current.CacheDirectory;
        var path = Path.Combine(exportDir, fileName);
        using var wb = new XLWorkbook();
        var ws1 = wb.AddWorksheet("الحوض");
        int r = 1;
        ws1.Cell(r,1).Value="المزرعة"; ws1.Cell(r++,2).Value=farm.Name;
        ws1.Cell(r,1).Value="اسم الحوض"; ws1.Cell(r++,2).Value=pond.Name;
        ws1.Cell(r,1).Value="المساحة (م²)"; ws1.Cell(r++,2).Value=pond.Area;
        ws1.Cell(r,1).Value="العمق (م)"; ws1.Cell(r++,2).Value=pond.Depth;
        ws1.Cell(r,1).Value="نوع المياه"; ws1.Cell(r++,2).Value=pond.WaterType;
        ws1.Cell(r,1).Value="عدد الأسماك"; ws1.Cell(r++,2).Value=pond.FishCount;
        var ws2 = wb.AddWorksheet("الملاحظات");
        ws2.Cell(1,1).Value="التاريخ";
        ws2.Cell(1,2).Value="الحرارة";
        ws2.Cell(1,3).Value="التغذية";
        ws2.Cell(1,4).Value="النفوق";
        ws2.Cell(1,5).Value="pH";
        ws2.Cell(1,6).Value="DO (mg/L)";
        ws2.Cell(1,7).Value="NH3 (mg/L)";
        ws2.Cell(1,8).Value="الملوحة (ppt)";
        ws2.Cell(1,9).Value="ملاحظات";
        int row = 2;
        foreach (var n in notes.OrderBy(n=>n.Date))
        {
            ws2.Cell(row,1).Value=n.Date;
            ws2.Cell(row,2).Value=n.Temperature;
            ws2.Cell(row,3).Value=n.Feeding;
            ws2.Cell(row,4).Value=n.Mortality;
            ws2.Cell(row,5).Value=n.PH;
            ws2.Cell(row,6).Value=n.DissolvedOxygen;
            ws2.Cell(row,7).Value=n.Ammonia;
            ws2.Cell(row,8).Value=n.Salinity;
            ws2.Cell(row,9).Value=n.Comment;
            row++;
        }
        wb.SaveAs(path);
        await Task.CompletedTask;
        return path;
    }
    private static string Sanitize(string s) => string.Join("_", (s ?? "Pond").Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries));
}
