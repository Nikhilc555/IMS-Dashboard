using IMS_Dashboard.Models.Entities;
using IMS_Dashboard.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IMS_Dashboard.Repositories.repos
{
    public class reportRepository : IreportRepository
    {
        private readonly ImsDbContext _context;
        public reportRepository(ImsDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<DailyQtyReport>> GetDailyInventories()
        {
            var last7Days = Enumerable.Range(0, 7)
                .Select(i => DateTime.UtcNow.Date.AddDays(-i))
                .ToList();

                        var reportData = await _context.ImportInventories
                            .Where(i => i.CreatedOn >= DateTime.UtcNow.Date.AddDays(-7) && i.IsActive == "Y")
                            .GroupBy(i => i.CreatedOn.Value.Date)
                            .Select(g => new DailyQtyReport
                            {
                                ReportDate = g.Key,
                                TotalQuantity = g.Sum(i => i.Qty ?? 0)
                            })
                            .ToListAsync();

                        // Ensure all dates appear in the report, even if they have 0 values
                        var finalReport = last7Days
                            .Select(date => reportData.FirstOrDefault(r => r.ReportDate == date)
                                ?? new DailyQtyReport { ReportDate = date, TotalQuantity = 0 })
                            .OrderBy(r => r.ReportDate)
                            .ToList();

            return finalReport;


        }
        public async Task<IEnumerable<DailyQtyReport>> GetPreviousnventories()
        {
            var last7Days = Enumerable.Range(7, 7) // Start from 8 days ago and take 7 days
                .Select(i => DateTime.UtcNow.Date.AddDays(-i))
                .ToList();


            var reportData = await _context.ImportInventories
                .Where(i => i.CreatedOn >= last7Days.Min() && i.CreatedOn <= last7Days.Max().AddDays(1) && i.IsActive == "Y")
                .GroupBy(i => i.CreatedOn.Value.Date)
                .Select(g => new DailyQtyReport
                {
                    ReportDate = g.Key,
                    TotalQuantity = g.Sum(i => i.Qty ?? 0)
                })
            .ToListAsync();

            var finalReport = last7Days
                            .Select(date => reportData.FirstOrDefault(r => r.ReportDate == date)
                                ?? new DailyQtyReport { ReportDate = date, TotalQuantity = 0 })
                            .OrderBy(r => r.ReportDate)
                            .ToList();

            return finalReport;


        }
        public async Task<List<DailyQtyReport>> GetMonthlyReport(int year)
        {
            var months = Enumerable.Range(1, 12) // Generate months from 1 to 12
                .Select(month => new DateTime(year, month, 1)) // Create first day of each month
                .ToList();

            var reportData = await _context.ImportInventories
                .Where(i => i.CreatedOn.HasValue
                            && i.CreatedOn.Value.Year == year
                            && i.IsActive == "Y")
                .GroupBy(i => new { i.CreatedOn.Value.Year, i.CreatedOn.Value.Month }) // Group by Year and Month
                .Select(g => new DailyQtyReport
                {
                    ReportDate = new DateTime(g.Key.Year, g.Key.Month, 1),
                    TotalQuantity = g.Sum(i => i.Qty ?? 0)
                })
                .ToListAsync();

            var finalReport = months
                .Select(date => reportData.FirstOrDefault(r => r.ReportDate.Month == date.Month)
                    ?? new DailyQtyReport { ReportDate = date, TotalQuantity = 0 }) // Default for missing months
                .OrderBy(r => r.ReportDate)
                .ToList();

            return finalReport;
        }
        public async Task<List<DailyQtyReport>> GetMonthlyShippedReport(int year)
        {
            var months = Enumerable.Range(1, 12) // Generate months from 1 to 12
                .Select(month => new DateTime(year, month, 1)) // Create first day of each month
                .ToList();

            var reportData = await _context.ImportInventories
                .Where(i => i.CreatedOn.HasValue
                            && i.CreatedOn.Value.Year == year
                            && i.IsActive == "Y" && i.Export_status)
                .GroupBy(i => new { i.CreatedOn.Value.Year, i.CreatedOn.Value.Month }) // Group by Year and Month
                .Select(g => new DailyQtyReport
                {
                    ReportDate = new DateTime(g.Key.Year, g.Key.Month, 1),
                    TotalQuantity = g.Sum(i => i.Qty ?? 0)
                })
                .ToListAsync();

            var finalReport = months
                .Select(date => reportData.FirstOrDefault(r => r.ReportDate.Month == date.Month)
                    ?? new DailyQtyReport { ReportDate = date, TotalQuantity = 0 }) // Default for missing months
                .OrderBy(r => r.ReportDate)
                .ToList();

            return finalReport;
        }

    }
}
