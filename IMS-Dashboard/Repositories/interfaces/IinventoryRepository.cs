using IMS_Dashboard.Models.Entities;

namespace IMS_Dashboard.Repositories.interfaces
{
    public interface IinventoryRepository
    {
        Task Create(ImportInventory inv);
    }
}
