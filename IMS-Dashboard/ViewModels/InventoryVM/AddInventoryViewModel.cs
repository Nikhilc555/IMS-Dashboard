using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace IMS_Dashboard.ViewModels.InventoryVM
{
    public class AddInventoryViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "CTN No is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "CTNNo must be greater than 0.")]
        public int CTNNo { get; set; }
        //public string? ShippingMark { get; set; }
        [Required(ErrorMessage = "Supplier is required.")]
        public int SupplierId { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        public int? ProductId { get; set; }
        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int QuantityAdded { get; set; }
        [Required(ErrorMessage = "Weight is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Weight must be greater than 0.")]
        public decimal Weight { get; set; }
        public List<SelectListItem>? Products { get; set; }
        public List<SelectListItem>? Suppliers { get; set; }
    }
}
