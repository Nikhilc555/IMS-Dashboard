using IMS_Dashboard.Services.InventoryServices.Interface;
using IMS_Dashboard.Services.ProductServices.Interface;
using IMS_Dashboard.Services.SupplierServices.Interface;
using IMS_Dashboard.Services.TransactionTypeServices.Interface;
using IMS_Dashboard.Services.TransactionTypeServices.Service;
using IMS_Dashboard.ViewModels.InventoryVM;
using IMS_Dashboard.ViewModels.ShipmentVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace IMS_Dashboard.Controllers
{
    public class ShipmentController : Controller
    {
        private readonly IInventoryService _inventoryService;
        private readonly IProductService _productService;
        private readonly ISupplierService _supplierService;
        private readonly ILogger<ShipmentController> _logger;

        public ShipmentController(IInventoryService inventoryService, IProductService productService, ISupplierService supplierService,
            ILogger<ShipmentController> logger)
        {
            _inventoryService = inventoryService;
            _productService = productService;
            _supplierService = supplierService;
            _logger = logger;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllPendingInventory()
        {
            try
            {
                _logger.LogInformation("Fetching all Inventories");

                var inventory = await _inventoryService.GetPendingInventory();
                return View(inventory);
            }
            catch (HttpRequestException ex)
            {
                // Log Exception
                _logger.LogError(ex, "Error fetching inventory from the API.");
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetInventoryForShipping(string shippingRef = null)
        {
            try
            {
                _logger.LogInformation("Fetching all Inventories");

                var model = new GetAllShipmentViewModel();

                model.ShippingRef = shippingRef;
                PopulateDropDownsNew(model);

                if (!string.IsNullOrEmpty(shippingRef))
                {
                    model.Inventory = await _inventoryService.GetPendingWithShipRef(shippingRef);

                }
                return View(model);
            }
            catch (HttpRequestException ex)
            {
                // Log Exception
                _logger.LogError(ex, "Error fetching inventory from the API.");
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetShippedDetails(string? from_date, string? to_date)
        {
            try
            {
                _logger.LogInformation("Fetching all Inventories");

                var inventory = await _inventoryService.GetShipmentwithdate(from_date, to_date);
                return View(inventory);
            }
            catch (HttpRequestException ex)
            {
                // Log Exception
                _logger.LogError(ex, "Error fetching inventory from the API.");
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateExportStatus(string updatedInventoryForShipment, string shippingRef)
        {
            if (!string.IsNullOrEmpty(updatedInventoryForShipment))
            {
                List<int> remainingIds = JsonConvert.DeserializeObject<List<int>>(updatedInventoryForShipment);

                // Get all existing inventory items
                //var allInventory = _context.ImportInventories.ToList();

                //foreach (var inventory in allInventory)
                //{
                //    if (!remainingIds.Contains(inventory.Id))
                //    {
                //        inventory.Status = "Deleted"; // Mark as deleted
                //        _context.ImportInventories.Update(inventory);
                //    }
                //}

                //_context.SaveChanges();

                try
                {
                    _logger.LogInformation("Attempting to update for shipment.");
                    bool result = await _inventoryService.UpdateInventoryForShipment(remainingIds, shippingRef);

                    if (result)
                    {
                        _logger.LogInformation("Shipment creation successful. Redirecting to GetInventoryForShipping.");
                        return RedirectToAction(nameof(GetInventoryForShipping));
                    }
                    else
                    {
                        _logger.LogWarning("Shipment creation failed.");
                        ModelState.AddModelError("", "An error occurred while creating the shipment.");
                        return RedirectToAction(nameof(GetInventoryForShipping));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while creating the shipment.");
                    ModelState.AddModelError("", "An error occurred while creating the shipment.");
                    return RedirectToAction(nameof(GetInventoryForShipping));
                }
            }

            return RedirectToAction(nameof(GetInventoryForShipping));
        }
        private async Task PopulateDropDownsNew(GetAllShipmentViewModel shipmentViewModel)
        {

            var shippingRef = GetWeeksForNext6Months();
            shipmentViewModel.ShippingRefOptions = shippingRef.Select(p => new SelectListItem
            {
                Value = p.ToString(),
                Text = p.ToString()
            }).ToList();
        }
        public IEnumerable<string> GetWeeksForNext6Months()
        {
            List<string> weeksList = new List<string>();

            DateTime today = DateTime.Now;
            DateTime startMonth = new DateTime(today.Year, today.Month, 1);

            for (int i = 0; i < 6; i++)
            {
                DateTime currentMonth = startMonth.AddMonths(i);
                string monthName = currentMonth.ToString("MMMM");

                for (int week = 1; week <= 4; week++)
                {
                    weeksList.Add($"{monthName} - Week {week}");
                }
            }

            return weeksList;
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
