using IMS_Dashboard.Services.SupplierServices.Interface;
using IMS_Dashboard.ViewModels.SuppliersVM;
using IMS_Dashboard.ViewModels.UserVM;
using Microsoft.AspNetCore.Mvc;

namespace IMS_Dashboard.Controllers
{
    [Route("[controller]")]
    public class SuppliersController : Controller
    {
        private readonly ISupplierService _supplierService;
        private readonly ILogger<SuppliersController> _logger;
        public SuppliersController(ISupplierService supplierService, ILogger<SuppliersController> logger)
        {
            _supplierService = supplierService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSuppliers()
        {
            try
            {
                _logger.LogInformation("Fetching all Suppliers");

                var suppliers = await _supplierService.GetAllSuppliers();
                return View(suppliers);
            }
            catch (HttpRequestException ex)
            {
                // Log Exception
                _logger.LogError(ex, "Error fetching suppliers from the API.");
                throw;
            }
        }

        [HttpGet("AddSupplier")]
        public IActionResult AddSupplier()
        {
            return View();
        }

        [HttpGet("EditSupplier")]
        public async Task<IActionResult> EditSupplier(int id)
        {
            var addSupplierViewModel = new AddSupplierViewModel();

            var supplier = await _supplierService.GetSupplierById(id);

            addSupplierViewModel.Id = supplier.id;
            addSupplierViewModel.ShipmentMark = supplier.shipment_mark;
            addSupplierViewModel.SupplierName = supplier.supplier_name;
            addSupplierViewModel.ContactName = supplier.ContactName;
            addSupplierViewModel.PhoneNumber = supplier.PhoneNumber;
            addSupplierViewModel.Email = supplier.Email;
            addSupplierViewModel.Address = supplier.Address;

            // Fetch dropdowns from the API
            return View(addSupplierViewModel);
        }

        [HttpPost("AddSupplier")]
        public async Task<IActionResult> AddSupplier(AddSupplierViewModel supplierViewModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors);
                foreach (var error in errors)
                {
                    _logger.LogError($"Model validation error: {error.ErrorMessage}");
                }

                return View(supplierViewModel);
            }

            try
            {
                _logger.LogInformation("Attempting to create a new supplier.");
                bool result = await _supplierService.CreateSupplier(supplierViewModel);

                if (result)
                {
                    _logger.LogInformation("Supplier creation successful. Redirecting to GetAllSuppliers.");
                    return RedirectToAction(nameof(GetAllSuppliers));
                }
                else
                {
                    _logger.LogWarning("user creation failed.");
                    ModelState.AddModelError("", "An error occurred while creating the user.");
                    return View(supplierViewModel);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the supplier.");
                ModelState.AddModelError("", "An error occurred while creating the supplier.");
                return View(supplierViewModel);
            }
        }

        [HttpPost("EditSupplier")]
        public async Task<IActionResult> EditSupplier(AddSupplierViewModel supplierViewModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors);
                foreach (var error in errors)
                {
                    _logger.LogError($"Model validation error: {error.ErrorMessage}");
                }

                return View(supplierViewModel);
            }

            try
            {
                _logger.LogInformation("Attempting to create a new supplier.");
                bool result = await _supplierService.UpdateSupplier(supplierViewModel);

                if (result)
                {
                    _logger.LogInformation("spplier creation successful. Redirecting to GetAllSuppliers.");
                    return RedirectToAction(nameof(GetAllSuppliers));
                }
                else
                {
                    _logger.LogWarning("supplier creation failed.");
                    ModelState.AddModelError("", "An error occurred while creating the supplier.");
                    return View(supplierViewModel);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the supplier.");
                ModelState.AddModelError("", "An error occurred while creating the ussupplierer.");
                return View(supplierViewModel);
            }
        }

        [HttpPost("DeleteSupplier")]
        public async Task<IActionResult> DeleteUser(int id)
        {

            try
            {
                _logger.LogInformation("Attempting to delete an user.");
                bool result = await _supplierService.DeleteSupplier(id);

                if (result)
                {
                    _logger.LogInformation("Supplier deletion successful. Redirecting to GetAllSuppliers.");
                    return RedirectToAction(nameof(GetAllSuppliers));
                }
                else
                {
                    _logger.LogWarning("Supplier deletion failed.");
                    ModelState.AddModelError("", "An error occurred while deleting the supplier.");
                    return RedirectToAction(nameof(GetAllSuppliers));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the supplier.");
                ModelState.AddModelError("", "An error occurred while deleting the supplier.");
                return RedirectToAction(nameof(GetAllSuppliers));
            }
        }

        //[HttpPost("CreateSupplier")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreateSupplier(CreateSupplierViewModel supplierViewModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        _logger.LogWarning("Invalid model state for create supplier.");
        //        return View(supplierViewModel);
        //    }

        //    try
        //    {
        //        _logger.LogInformation("Attempting to create a new supplier.");
        //        var createdSupplier = await _supplierService.CreateSupplier(supplierViewModel);

        //        if(createdSupplier != null)
        //        {
        //            _logger.LogInformation("Spplier creation successsfull. Redirecting to GetAllSuppliers.");
        //            return RedirectToAction(nameof(GetAllSuppliers));
        //        }
        //        else
        //        {
        //            _logger.LogWarning("Supplier creation failed.");
        //            ModelState.AddModelError("", "An error occurred while creating supplier.");
        //            return View(supplierViewModel);
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        _logger.LogError(ex, "Error occurred while creating supplier.");
        //        return StatusCode(500, "Internal Server Error.");
        //    }
        //}
    }
}
