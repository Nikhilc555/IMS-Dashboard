using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS_Dashboard.Models.Entities;

[Table("import_inventory")]
public partial class ImportInventory
{
    public int Id { get; set; }

    [Column("ctn_no")]
    public int? CtnNo { get; set; }

    //[Column("shipping_mark")]
    //public string? ShippingMark { get; set; }

    [Column("supplier_id")]
    public int? SupplierId { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("product_id")]
    public int? ProductId { get; set; }

    public int? Qty { get; set; }

    public decimal? Weight { get; set; }

    [Column("created_on")]
    public DateTime? CreatedOn { get; set; }

    [Column("created_by")]
    public int? CreatedBy { get; set; }

    [Column("export_status")]
    public bool Export_status { get; set; }

    [Column("shipping_ref")]
    public string? Shipping_ref { get; set; }

    [Column("shipped_on")]
    public DateTime? Shipped_on { get; set; }

    [Column("shipped_by")]
    public int? Shipped_by { get; set; }

    [Column("updated_on")]
    public DateTime? Updated_on { get; set; }

    [Column("updated_by")]
    public int? Updated_by { get; set; }

    [Column("is_active")]
    public string? IsActive { get; set; }
}
