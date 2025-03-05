using System.ComponentModel.DataAnnotations.Schema;

namespace IMS_Dashboard.Models.Entities
{
    public class Suppliers
    {
        public int Id { get; set; }
        [Column("supplier_name")]
        public string? SupplierName { get; set; }
        [Column("is_active")]
        public bool? IsActive { get; set; }
        [Column("created_on")]
        public DateTime? CreatedOn { get; set; }
        [Column("created_by")]
        public int? CreatedBy { get; set; }
        [Column("shipment_marks")]
        public string? ShipmentMarks { get; set; }
        [Column("contact_person")]
        public string? ContactPerson { get; set; }
        [Column("phone")]
        public string? Phone { get; set; }
        [Column("email")]
        public string? Email { get; set; }
        [Column("address")]
        public string? Address { get; set; }
        [Column("updated_on")]
        public DateTime? UpdatedOn { get; set; }
        [Column("updated_by")]
        public int? UpdatedBy { get; set; }
    }
}
