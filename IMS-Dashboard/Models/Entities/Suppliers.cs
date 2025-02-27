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
    }
}
