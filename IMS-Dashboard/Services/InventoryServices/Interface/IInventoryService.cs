using IMS_Dashboard.ViewModels.InventoryVM;

namespace IMS_Dashboard.Services.InventoryServices.Interface
{
    public interface IInventoryService
    {
        Task<int> GetAllInventoryCount();
        Task<IEnumerable<DisplayInventoryViewModel>> GetAllInventories();
        Task<IEnumerable<GetAllInventoryViewModel>> GetAllInventory();
        Task<GetAllInventoryViewModel> GetInventoryById(int id);
        Task<IEnumerable<GetAllInventoryViewModel>> GetPendingInventory();
        Task<IEnumerable<GetAllInventoryViewModel>> GetAllInventorywithdate(string from_date, string to_date);
        Task<IEnumerable<DisplayRecentInventoryViewModel>> GetRecentInventories();
        Task<bool> CreateInventory(CreateInventoryViewModel inventoryViewModel);
        Task<bool> AddInventory(AddInventoryViewModel inventoryViewModel);
        Task<bool> UpdateInventoryForShipment(List<int> remainingIds, string shippingRef);
        Task<bool> EditInventory(AddInventoryViewModel inventoryViewModel);
        Task<bool> DeleteInventory(int id);
    }
}
