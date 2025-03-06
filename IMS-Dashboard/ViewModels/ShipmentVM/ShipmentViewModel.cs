using System.ComponentModel.DataAnnotations;

namespace IMS_Dashboard.ViewModels.ShipmentVM
{
    public class ShipmentViewModel
    {
        public int Id { get; set; }

        [Display(Name = "CTN No")]
        public int? CTNNo { get; set; }

        //[Display(Name = "Shipping Mark")]
        //public string ShippingMark { get; set; }
        public string? Description { get; set; }
        public int? ProductId { get; set; }
        public int? SupplierId { get; set; }

        [Display(Name = "Supplier Name")]
        public string Supplier { get; set; }

        [Display(Name = "Product Name")]
        public string Product { get; set; }

        [Display(Name = "Quantity")]
        public int? Quantity { get; set; }
        public decimal? Weight { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string Created_by { get; set; }

        [Display(Name = "Shipping Ref")]
        public string ShippingRef { get; set; }

        [Display(Name = "Shipped On")]
        public DateTime? ShippedOn { get; set; }

        [Display(Name = "Shipped By")]
        public string Shipped_by { get; set; }
        public string Export_Status { get; set; }
        public string from_date { get; set; }
        public string to_date { get; set; }
    }
}
