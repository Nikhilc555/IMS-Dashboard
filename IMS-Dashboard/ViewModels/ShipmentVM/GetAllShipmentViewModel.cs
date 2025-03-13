using IMS_Dashboard.ViewModels.InventoryVM;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IMS_Dashboard.ViewModels.ShipmentVM
{
    public class GetAllShipmentViewModel
    {
        public string ShippingRef { get; set; }
        public IEnumerable<GetAllInventoryViewModel> Inventory { get; set; }
        public List<SelectListItem>? ShippingRefOptions { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
    }
}
