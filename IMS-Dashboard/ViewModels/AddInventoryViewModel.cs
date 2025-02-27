using Microsoft.AspNetCore.Mvc.Rendering;

namespace IMS_Dashboard.ViewModels.InventoryVM
{
    public class AddInventoryViewModel
    {
        public int Id { get; set; }
        public int CTNNo { get; set; }
        public string ShippingMark { get; set; }
        public int SupplierId { get; set; }
        public int ProductId { get; set; }
        public int QuantityAdded { get; set; }
        public decimal Weight { get; set; }
        public List<SelectListItem>? Products { get; set; }
        public List<SelectListItem>? Suppliers { get; set; }
    }
}
