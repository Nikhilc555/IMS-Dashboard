using System.ComponentModel.DataAnnotations.Schema;

namespace IMS_Dashboard.Models.Entities
{
    [Table("shipment_list")]
    public class ShipmentList
    {
        public int Id { get; set; }

        [Column("shipment_name")]
        public string? ShipmentName { get; set; }

        [Column("created_on")]
        public DateTime? CreatedOn { get; set; }

        [Column("created_by")]
        public int? CreatedBy { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }
    }
}
