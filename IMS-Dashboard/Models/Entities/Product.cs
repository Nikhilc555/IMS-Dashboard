using System;
using System.Collections.Generic;

namespace IMS_Dashboard;

public partial class Product
{
    public int Id { get; set; }

    public string? ProductName { get; set; }

    public string? ProductCode { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? Category { get; set; }

    public int? Manufacurer { get; set; }

    public int? Uom { get; set; }

    public bool? IsActive { get; set; }
}
