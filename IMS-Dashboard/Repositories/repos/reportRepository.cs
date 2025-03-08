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
    }
}
