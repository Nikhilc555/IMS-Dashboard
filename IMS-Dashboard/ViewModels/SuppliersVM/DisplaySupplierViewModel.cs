using System.ComponentModel.DataAnnotations;

namespace IMS_Dashboard.ViewModels.SuppliersVM
{
    public class DisplaySupplierViewModel
    {
        //public Guid SupplierId { get; set; }
        //[Display(Name = "Supplier Name")]
        //public string SupplierName { get; set; }

        [Display(Name = "Contact Person")]
        public string ContactName { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Notes")]
        public string? Notes { get; set; }


        public int id { get; set; }

        [Display(Name = "Supplier Name")]
        public string supplier_name { get; set; }

        [Display(Name = "Shipment Mark")]
        public string shipment_mark { get; set; }

        [Display(Name = "Created On")]
        public DateTime? CreatedOn { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Updated On")]
        public DateTime? UpdatedOn { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }
    }
}
