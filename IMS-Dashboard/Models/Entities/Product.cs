using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMS_Dashboard;

public partial class Product
{
    public int Id { get; set; }
    [Column("product_name")]
    public string? ProductName { get; set; }
    [Column("product_code")]
    public string? ProductCode { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? Category { get; set; }

    public int? Manufacurer { get; set; }

    public int? Uom { get; set; }
    [Column("is_active")]
    public bool? IsActive { get; set; }
}
