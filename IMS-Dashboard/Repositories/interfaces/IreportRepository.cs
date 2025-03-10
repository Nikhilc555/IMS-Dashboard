using IMS_Dashboard.Models.Entities;

namespace IMS_Dashboard.Repositories.interfaces
{
    public interface IreportRepository
    {
        Task<IEnumerable<DailyQtyReport>> GetDailyInventories();
        Task<IEnumerable<DailyQtyReport>> GetPreviousnventories();
        Task<List<DailyQtyReport>> GetMonthlyReport(int year);
        Task<List<DailyQtyReport>> GetMonthlyShippedReport(int year);
    }
}
