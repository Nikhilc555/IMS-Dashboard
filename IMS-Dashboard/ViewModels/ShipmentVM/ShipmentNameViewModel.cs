using System.ComponentModel.DataAnnotations;

namespace IMS_Dashboard.ViewModels.ShipmentVM
{
    public class ShipmentNameViewModel
    {
        public int Id { get; set; }
        public string? ShipmentName { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public bool IsActive { get; set; }
    }
}
