using IMS_Dashboard.Services.InventoryServices.Interface;
using IMS_Dashboard.Services.ProductServices.Interface;
using IMS_Dashboard.Services.SupplierServices.Interface;
using IMS_Dashboard.Services.TransactionTypeServices.Interface;
using IMS_Dashboard.ViewModels.InventoryVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IMS_Dashboard.Controllers
{
    [Route("[controller]")]
    public class InventoriesController : Controller
    {
        private readonly IInventoryService _inventoryService;
        private readonly IProductService _productService;
        private readonly ISupplierService _supplierService;
        private readonly ITransactionTypeService _transactionTypeService;
        private readonly ILogger<InventoriesController> _logger;
        public InventoriesController(IInventoryService inventoryService, IProductService productService, ISupplierService supplierService,
            ITransactionTypeService transactionTypeService, ILogger<InventoriesController> logger)
        {
            _inventoryService = inventoryService;
            _productService = productService;
            _supplierService = supplierService;
            _transactionTypeService = transactionTypeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInventories(string? from_date, string? to_date)
        {
            try
            {
                _logger.LogInformation("Fetching all Inventories");

                if (from_date== null || to_date == null)
                {
                    var inventory = await _inventoryService.GetAllInventory();
                    return View(inventory);
                }
                else
                {
                    var inventory = await _inventoryService.GetAllInventorywithdate(from_date, to_date);
                    return View(inventory);
                }
            }
            catch (HttpRequestException ex)
            {
                // Log Exception
                _logger.LogError(ex, "Error fetching inventory from the API.");
                throw;
            }
        }

        [HttpGet("CreateInventory")]
        public async Task<IActionResult> CreateInventory()
        {
            var createInventoryViewModel = new CreateInventoryViewModel();

            // Fetch dropdowns from the API
            await PopulateDropDowns(createInventoryViewModel);
            return View(createInventoryViewModel);
        }

        [HttpGet("AddInventory")]
        public async Task<IActionResult> AddInventory()
        {
            var addInventoryViewModel = new AddInventoryViewModel();

            // Fetch dropdowns from the API
            await PopulateDropDownsNew(addInventoryViewModel);
            return View(addInventoryViewModel);
        }

        [HttpPost("CreateInventory")]
        public async Task<IActionResult> CreateInventory(CreateInventoryViewModel inventoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors);
                foreach (var error in errors)
                {
                    _logger.LogError($"Model validation error: {error.ErrorMessage}");
                }

                await PopulateDropDowns(inventoryViewModel);
                return View(inventoryViewModel);
            }

            try
            {
                _logger.LogInformation("Attempting to create a new inventory.");
                bool result = await _inventoryService.CreateInventory(inventoryViewModel);

                if (result)
                {
                    _logger.LogInformation("Inventory creation successful. Redirecting to GetAllInventories.");
                    return RedirectToAction(nameof(GetAllInventories));
                }
                else
                {
                    _logger.LogWarning("Inventory creation failed.");
                    ModelState.AddModelError("", "An error occurred while creating the inventory.");
                    await PopulateDropDowns(inventoryViewModel);
                    return View(inventoryViewModel);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the inventory.");
                ModelState.AddModelError("", "An error occurred while creating the inventory.");
                await PopulateDropDowns(inventoryViewModel);
                return View(inventoryViewModel);
            }
        }

        [HttpPost("AddInventory")]
        public async Task<IActionResult> AddInventory(AddInventoryViewModel inventoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors);
                foreach (var error in errors)
                {
                    _logger.LogError($"Model validation error: {error.ErrorMessage}");
                }

                await PopulateDropDownsNew(inventoryViewModel);
                return View(inventoryViewModel);
            }

            try
            {
                _logger.LogInformation("Attempting to create a new inventory.");
                bool result = await _inventoryService.AddInventory(inventoryViewModel);

                if (result)
                {
                    _logger.LogInformation("Inventory creation successful. Redirecting to GetAllInventories.");
                    return RedirectToAction(nameof(GetAllInventories));
                }
                else
                {
                    _logger.LogWarning("Inventory creation failed.");
                    ModelState.AddModelError("", "An error occurred while creating the inventory.");
                    await PopulateDropDownsNew(inventoryViewModel);
                    return View(inventoryViewModel);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the inventory.");
                ModelState.AddModelError("", "An error occurred while creating the inventory.");
                await PopulateDropDownsNew(inventoryViewModel);
                return View(inventoryViewModel);
            }
        }

        private async Task PopulateDropDowns(CreateInventoryViewModel inventoryViewModel)
        {
            var products = await _productService.GetAllProducts();
            inventoryViewModel.Products = products.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.ProductName
            }).ToList();

            var suppliers = await _supplierService.GetAllSuppliers();
            inventoryViewModel.Suppliers = suppliers.Select(s => new SelectListItem
            {
                Value = s.id.ToString(),
                Text = s.supplier_name
            }).ToList();

            var transactionTypes = await _transactionTypeService.GetAllTransactionTypes();
            inventoryViewModel.TransactionTypes = transactionTypes.Select(t => new SelectListItem
            {
                Value = t.TransactionTypeId.ToString(),
                Text = t.Name
            }).ToList();
        }
        private async Task PopulateDropDownsNew(AddInventoryViewModel inventoryViewModel)
        {
            var products = await _productService.GetAllProducts();
            inventoryViewModel.Products = products.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.ProductName
            }).ToList();

            var suppliers = await _supplierService.GetAllSuppliers();
            inventoryViewModel.Suppliers = suppliers.Select(p => new SelectListItem
            {
                Value = p.id.ToString(),
                Text = p.supplier_name
            }).ToList();
        }
    }
}
