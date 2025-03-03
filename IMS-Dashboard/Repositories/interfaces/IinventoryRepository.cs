using IMS_Dashboard.Models.Entities;

namespace IMS_Dashboard.Repositories.interfaces
{
    public interface IinventoryRepository
    {
        Task Create(ImportInventory inv);
        Task<IEnumerable<ImportInventory>> GetAll();
        Task<IEnumerable<ImportInventory>> GetAllPending();
        Task<IEnumerable<ImportInventory>> GetAllWithDate(string from_date, string to_date);
        Task<bool> UpdateForShipment(List<int> remainingIds);
    }
}
