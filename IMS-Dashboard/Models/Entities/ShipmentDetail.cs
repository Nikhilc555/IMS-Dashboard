using System;
using System.Collections.Generic;

namespace IMS_Dashboard.Models.Entities;

public partial class ShipmentDetail
{
    public int Id { get; set; }

    public int? CtnNo { get; set; }

    public string? ShippingMark { get; set; }

    public int? ProductId { get; set; }

    public int? Qty { get; set; }

    public decimal? Weight { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }
}
