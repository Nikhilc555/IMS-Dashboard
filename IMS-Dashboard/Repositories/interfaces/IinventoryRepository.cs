using IMS_Dashboard.Models.Entities;

namespace IMS_Dashboard.Repositories.interfaces
{
    public interface IinventoryRepository
    {
        Task Create(ImportInventory inv);
        Task Update(ImportInventory inv);
        Task Delete(int id);
        Task<IEnumerable<ImportInventory>> GetAll();
        Task<ImportInventory> GetById(int id);
        Task<IEnumerable<ImportInventory>> GetAllPending();
        Task<IEnumerable<ImportInventory>> GetAllWithDate(string from_date, string to_date);
        Task<IEnumerable<ImportInventory>> GetShipmentWithDate(string from_date, string to_date);
        Task<bool> UpdateForShipment(List<int> remainingIds, string shippingRef);
        Task<int> GetShippedInventoryCount();
        Task<int> GetPendingInventoryCount();
    }
}
