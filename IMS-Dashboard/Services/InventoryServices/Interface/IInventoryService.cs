using IMS_Dashboard.ViewModels.InventoryVM;
using IMS_Dashboard.ViewModels.ShipmentVM;

namespace IMS_Dashboard.Services.InventoryServices.Interface
{
    public interface IInventoryService
    {
        Task<int> GetAllInventoryCount();
        Task<IEnumerable<DisplayInventoryViewModel>> GetAllInventories();
        Task<IEnumerable<GetAllInventoryViewModel>> GetAllInventory();
        Task<GetAllInventoryViewModel> GetInventoryById(int id);
        Task<IEnumerable<GetAllInventoryViewModel>> GetPendingInventory();
        Task<IEnumerable<GetAllInventoryViewModel>> GetPendingInventoryWithDate(string from_date, string to_date);
        Task<IEnumerable<GetAllInventoryViewModel>> GetPendingWithShipRef(string ShippingRef);
        Task<IEnumerable<ShipmentNameViewModel>> GetAllShipmentNames();
        Task<IEnumerable<ShipmentNameViewModel>> GetAllShipmentList();
        Task<IEnumerable<GetAllInventoryViewModel>> GetAllInventorywithdate(string from_date, string to_date);
        Task<IEnumerable<GetAllInventoryViewModel>> GetShipmentwithdate(string from_date, string to_date, int shippingRef);
        Task<string> AddShipment(AddShipmentNameViewModel shipmentViewModel);
        Task<IEnumerable<DisplayRecentInventoryViewModel>> GetRecentInventories();
        Task<bool> CreateInventory(CreateInventoryViewModel inventoryViewModel);
        Task<string> AddInventory(AddInventoryViewModel inventoryViewModel);
        Task<bool> UpdateInventoryForShipment(List<int> remainingIds, string shippingRef);
        Task<string> EditInventory(AddInventoryViewModel inventoryViewModel);
        Task<bool> DeleteInventory(int id);
        Task<int> GetShippedInventoryCountAsync();
        Task<int> GetPendingInventoryCountAsync();
        Task<int> GetShipmentCountAsync();
        Task<int> GetPendingShipmentCountAsync();
    }
}
