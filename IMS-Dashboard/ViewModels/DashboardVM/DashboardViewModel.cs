using IMS_Dashboard.ViewModels.CustomersVM;
using IMS_Dashboard.ViewModels.InventoryVM;
using IMS_Dashboard.ViewModels.OrderVM;
using IMS_Dashboard.ViewModels.SuppliersVM;
using IMS_Dashboard.Models.Entities;

namespace IMS_Dashboard.ViewModels.DashboardVM
{
    public class DashboardViewModel
    {
        public int CustomerCount { get; set; }
        public int ProductCount { get; set; }
        public int SupplierCount { get; set; }
        public int InventoryCount { get; set; }
        public int OrderCount { get; set; }
        public int UserCount { get; set; }
        public int ShippedCount { get; set; }
        public int PendingCount { get; set; }
        public int PendingOrderCount { get; set; }
        public int CompletedOrderCount { get; set; }
        public int DeliveredOrderCount { get; set; }
        public int TotalOrderCount { get; set; }
        public int TotalInventoryCount { get; set; }
        public int SelectedYear { get; set; }

        public IEnumerable<DisplayCustomerViewModel>? top5Customers { get; set; }
        public IEnumerable<DisplaySupplierViewModel>? top5Suppliers { get; set; }
        public IEnumerable<DisplayRecentOrdersViewModel>? recentOrders { get; set; }
        public IEnumerable<DisplayRecentInventoryViewModel>? recentInventory { get; set; }
        public IEnumerable<DailyQtyReport>? dailyInventory { get; set; }
        public IEnumerable<DailyQtyReport>? previousInventory { get; set; }
        public IEnumerable<DailyQtyReport>? monthlyInventory { get; set; }
        public IEnumerable<DailyQtyReport>? monthlyShippedInventory { get; set; }
    }
}
