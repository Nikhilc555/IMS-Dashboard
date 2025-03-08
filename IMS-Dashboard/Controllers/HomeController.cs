using IMS_Dashboard.Models.Entities;
using IMS_Dashboard.Repositories.interfaces;
using IMS_Dashboard.Services.CustomerServices.Interface;
using IMS_Dashboard.Services.InventoryServices.Interface;
using IMS_Dashboard.Services.OrderServices.Interface;
using IMS_Dashboard.Services.ProductServices.Interface;
using IMS_Dashboard.Services.SupplierServices.Interface;
using IMS_Dashboard.Services.UserServices.Interface;
using IMS_Dashboard.Services.UserServices.Service;
using IMS_Dashboard.ViewModels.CustomersVM;
using IMS_Dashboard.ViewModels.DashboardVM;
using IMS_Dashboard.ViewModels.InventoryVM;
using IMS_Dashboard.ViewModels.OrderVM;
using IMS_Dashboard.ViewModels.SuppliersVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMS_Dashboard.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        private readonly ISupplierService _supplierService;
        private readonly IInventoryService _inventoryService;
        private readonly IOrderService _orderService;
        private readonly IuserService _userService;
        private readonly IreportRepository _reportRepo;

        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger, ICustomerService customerService, 
            IProductService productService, ISupplierService supplierService, IInventoryService inventoryService,
            IOrderService orderService, IuserService userService, IreportRepository reportRepo)
        {
            _customerService = customerService;
            _productService = productService;
            _supplierService = supplierService;
            _inventoryService = inventoryService;
            _orderService = orderService;
            _userService = userService;
            _reportRepo = reportRepo;

            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                //var customerCount = await _customerService.GetAllCustomersCountAsync();
                //var productCount = await _productService.GetAllProductsCount();
                var supplierCount = await _supplierService.GetAllSuppliersCountAsync();
                var userCount = await _userService.GetAllUserCountAsync();
                var shippedCount = await _inventoryService.GetShippedInventoryCountAsync();
                var pendingCount = await _inventoryService.GetPendingInventoryCountAsync();
                var shipmentCount = await _inventoryService.GetShipmentCountAsync();
                var pendingshipmentCount = await _inventoryService.GetPendingShipmentCountAsync();
                //var inventoryCount = await _inventoryService.GetAllInventoryCount();
                //var orderCount = await _orderService.GetAllOrdersCount();
                //var pendingOrderCount = await _orderService.GetOrdersCountByOrderStatus("Pending");
                //var completedOrderCount = await _orderService.GetOrdersCountByOrderStatus("Completed");
                //var delieverdOrderCount = await _orderService.GetOrdersCountByOrderStatus("Delivered");

                //var top5customer = await _customerService.GetAllCustomers();
                //var top5supplier = await _supplierService.GetAllSuppliers();
                //var recentOrders = await _orderService.GetRecentOrders(5);

                //var recentInventories = await _inventoryService.GetRecentInventories();

                var lastWeekInventory = await _reportRepo.GetDailyInventories();
                var previousWeekInventory = await _reportRepo.GetPreviousnventories();

                // Create ViewModel
                var dashboardViewModel = new DashboardViewModel
                {
                    CustomerCount = 0,
                    ProductCount = 0,
                    SupplierCount = supplierCount,
                    UserCount = userCount,
                    ShippedCount = shippedCount,
                    PendingCount = pendingCount,
                    InventoryCount = 0,
                    OrderCount = 0,
                    PendingOrderCount = pendingshipmentCount,
                    CompletedOrderCount = shipmentCount,
                    DeliveredOrderCount = 0,
                    TotalOrderCount = shipmentCount + pendingshipmentCount,

                    top5Customers = null,
                    top5Suppliers = null,
                    recentOrders = null,
                    recentInventory = null,
                    dailyInventory = lastWeekInventory,
                    previousInventory = previousWeekInventory
                };

                // Pass the ViewModel to the View
                return View(dashboardViewModel);
            }
            catch (Exception ex)
            {
                var dashboard = new DashboardViewModel
                {
                    CustomerCount = 0,
                    ProductCount = 0,
                    SupplierCount = 0,
                    InventoryCount = 0,
                    OrderCount = 0,
                    PendingOrderCount = 0,
                    CompletedOrderCount = 0,
                    DeliveredOrderCount = 0,
                    TotalOrderCount = 0,
                    top5Customers = Enumerable.Empty<DisplayCustomerViewModel>(),
                    top5Suppliers = Enumerable.Empty<DisplaySupplierViewModel>(),
                    recentOrders = Enumerable.Empty<DisplayRecentOrdersViewModel>(),
                    recentInventory = Enumerable.Empty<DisplayRecentInventoryViewModel>(),
                    dailyInventory = Enumerable.Empty<DailyQtyReport>()
                };

                return View(dashboard);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult SignalR()
        {
            return View();
        }
    }
}
