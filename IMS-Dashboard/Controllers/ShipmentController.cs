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
        public async Task<IActionResult> GetAllPendingInventory(string? from_date, string? to_date)
        {
            try
            {
                _logger.LogInformation("Fetching all Inventories");

                GetAllShipmentViewModel shipviewmodel = new GetAllShipmentViewModel();

                if (from_date == null || to_date == null)
                {

                    var inventory = await _inventoryService.GetPendingInventory();

                    shipviewmodel.Inventory = inventory;
                    shipviewmodel.from_date = from_date;
                    shipviewmodel.to_date = to_date;
                    return View(shipviewmodel);
                }
                else
                {
                    var inventory = await _inventoryService.GetPendingInventoryWithDate(from_date, to_date);

                    shipviewmodel.Inventory = inventory;
                    shipviewmodel.from_date = from_date;
                    shipviewmodel.to_date = to_date;
                    return View(shipviewmodel);
                }
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
        public async Task<IActionResult> GetShippedDetails(string? from_date, string? to_date, int shippingRef)
        {
            try
            {
                _logger.LogInformation("Fetching all Inventories");

                GetAllShipmentViewModel shipviewmodel = new GetAllShipmentViewModel();


                var inventory = await _inventoryService.GetShipmentwithdate(from_date, to_date, shippingRef);

                shipviewmodel.Inventory = inventory;
                shipviewmodel.ShippingRef = shippingRef.ToString();
                shipviewmodel.from_date = from_date;
                shipviewmodel.to_date = to_date;
                PopulateDropDowns(shipviewmodel);

                return View(shipviewmodel);
            }
            catch (HttpRequestException ex)
            {
                // Log Exception
                _logger.LogError(ex, "Error fetching inventory from the API.");
                throw;
            }
        }



        [HttpGet("AddShipmentList")]
        public async Task<IActionResult> AddShipmentList()
        {
            try
            {
                _logger.LogInformation("Add shipment name");

                var model = new AddShipmentNameViewModel();

                return View(model);
            }
            catch (HttpRequestException ex)
            {
                // Log Exception
                _logger.LogError(ex, "Error in add shipment name.");
                throw;
            }
        }


        [HttpPost("AddShipmentList")]
        public async Task<IActionResult> AddShipmentList(AddShipmentNameViewModel shipmentnameViewModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors);
                foreach (var error in errors)
                {
                    _logger.LogError($"Model validation error: {error.ErrorMessage}");
                }

                return View(shipmentnameViewModel);
            }

            try
            {
                _logger.LogInformation("Attempting to create a new shipment name.");
                string result = await _inventoryService.AddShipment(shipmentnameViewModel);

                if (result.ToString() == "Shipment Name added successfully")
                {
                    _logger.LogInformation("Shipment Name added successfully");
                    ModelState.Clear();
                    shipmentnameViewModel = new AddShipmentNameViewModel();
                    return View(shipmentnameViewModel);
                }
                else
                {
                    _logger.LogWarning("Shipment Name creation failed.");
                    ModelState.AddModelError("", result.ToString());

                    return View(shipmentnameViewModel);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the shipment name.");
                ModelState.AddModelError("", "An error occurred while creating the shipment name.");
                return View(shipmentnameViewModel);
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

            TempData["SuccessMessage"] = "Shipment completed successfully!";
            //_toastNotification.AddSuccessToastMessage("Shipment completed successfully!");
            return RedirectToAction(nameof(GetInventoryForShipping));
        }
        private async Task PopulateDropDownsNew(GetAllShipmentViewModel shipmentViewModel)
        {

            //var shippingRef = GetWeeksForNext6Months();
            //shipmentViewModel.ShippingRefOptions = shippingRef.Select(p => new SelectListItem
            //{
            //    Value = p.ToString(),
            //    Text = p.ToString()
            //}).ToList();
            var shipmentlist = await _inventoryService.GetAllShipmentNames();
            shipmentViewModel.ShippingRefOptions = shipmentlist.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.ShipmentName
            }).ToList();
        }
        private async Task PopulateDropDowns(GetAllShipmentViewModel shipmentViewModel)
        {

            //var shippingRef = GetWeeksForNext6Months();
            //shipmentViewModel.ShippingRefOptions = shippingRef.Select(p => new SelectListItem
            //{
            //    Value = p.ToString(),
            //    Text = p.ToString()
            //}).ToList();
            var shipmentlist = await _inventoryService.GetAllShipmentList();
            shipmentViewModel.ShippingRefOptions = shipmentlist.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.ShipmentName
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
