using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS_Dashboard.ViewModels.ShipmentVM
{
    public class AddShipmentNameViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Shipment Name")]
        public string? ShipmentName { get; set; }

        [Display(Name = "Created On")]
        public DateTime? CreatedOn { get; set; }

        [Display(Name = "Created By")]
        public string? CreatedBy { get; set; }

        [Display(Name = "Status")]
        public bool IsActive { get; set; }
    }
}
