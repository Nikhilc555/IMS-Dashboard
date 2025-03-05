using System.ComponentModel.DataAnnotations;

namespace IMS_Dashboard.ViewModels.SuppliersVM
{
    public class AddSupplierViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Shipment Marks is required.")]
        [Display(Name = "Shipment Mark")]
        public string ShipmentMark { get; set; }

        [Required(ErrorMessage = "Supplier Name is required.")]
        [Display(Name = "Supplier Name")]
        public string SupplierName { get; set; }

        [Required(ErrorMessage = "Contact Name is required.")]
        [Display(Name = "Contact Person")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [Display(Name = "Address")]
        public string Address { get; set; }
    }
}
