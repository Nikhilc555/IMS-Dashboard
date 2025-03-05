using IMS_Dashboard.Models.Entities;
using IMS_Dashboard.Repositories.interfaces;
using IMS_Dashboard.Services.CategoryServices.Service;
using IMS_Dashboard.Services.InventoryServices.Service;
using IMS_Dashboard.Services.SupplierService.Interface;
using IMS_Dashboard.ViewModels.CategoryVM;
using IMS_Dashboard.ViewModels.SuppliersVM;
using IMS_Dashboard.ViewModels.UserVM;
using Microsoft.Extensions.Caching.Memory;
using static System.Net.WebRequestMethods;

namespace IMS_Dashboard.Services.SupplierService.Service
{
    public class supplierService : IsupplierService
    {
        private readonly IsupplierRepository _supplierRepo;
        private readonly IuserRepository _userRepo;
        private readonly IMemoryCache _cache;
        private readonly ILogger<CategoryService> _logger;
        private const string cacheKey = "SupplierList";
        public supplierService(IsupplierRepository supplierRepo, IuserRepository userRepo)
        {
            _supplierRepo = supplierRepo;
            _userRepo = userRepo;

        }
        public async Task<IEnumerable<DisplaySupplierViewModel>> GetAllSuppliers()
        {
            if (_cache.TryGetValue(cacheKey, out IEnumerable<DisplaySupplierViewModel> cachedSupplier))
            {
                _logger.LogInformation("Returning cached category list.");
                return cachedSupplier;
            }

            try
            {
                var categories = await _supplierRepo.GetAll();
                return categories.Select(c => new DisplaySupplierViewModel
                {
                    id = c.Id,
                    shipment_mark = c.ShipmentMarks,
                    supplier_name = c.SupplierName,
                    ContactName = c.ContactPerson,
                    PhoneNumber = c.Phone,
                    Email = c.Email,
                    Address = c.Address
                }).ToList();
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Network issue occurred while contacting the Supplier API.");
                throw new CategoryServiceException("Network error occurred while fetching categories.", httpEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in GetAllCategories");
                throw new CategoryServiceException("An unexpected error occurred while fetching categories.", ex);
            }
        }

        public async Task<DisplaySupplierViewModel> GetSupplierById(int id)
        {
            if (_cache.TryGetValue(cacheKey, out DisplaySupplierViewModel cachedsupplier))
            {
                _logger.LogInformation("Returning cached supplier list.");
                return cachedsupplier;
            }

            try
            {

                var suppliers = await _supplierRepo.Get(id);


                var created_by = await _userRepo.Get(suppliers.CreatedBy);

                DisplaySupplierViewModel user = new DisplaySupplierViewModel();
                if (suppliers != null)
                {
                    user = new DisplaySupplierViewModel
                    {
                        id = suppliers.Id,
                        shipment_mark = suppliers.ShipmentMarks,
                        supplier_name = suppliers.SupplierName,
                        ContactName = suppliers.ContactPerson,
                        PhoneNumber = suppliers.Phone,
                        Email = suppliers.Email,
                        Address = suppliers.Address,
                        CreatedOn = suppliers.CreatedOn,
                        CreatedBy = created_by.NameOfUser
                    };

                }

                return user;
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Network issue occurred while contacting the supplier API.");
                throw new InventoryServiceException("Network error occurred while fetching supplier.", httpEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in GetAllsupplier");
                throw new InventoryServiceException("An unexpected error occurred while fetching supplier.", ex);
            }
        }

        public async Task<bool> CreateUser(AddSupplierViewModel supplierViewModel)
        {
            if (supplierViewModel == null)
            {
                throw new ArgumentNullException(nameof(supplierViewModel));
            }

            try
            {
                var supplierToCreate = new Suppliers
                {
                    ShipmentMarks = supplierViewModel.ShipmentMark,
                    SupplierName = supplierViewModel.SupplierName,
                    ContactPerson = supplierViewModel.ContactName,
                    Phone = supplierViewModel.PhoneNumber,
                    Email = supplierViewModel.Email,
                    Address = supplierViewModel.Address,
                    IsActive = true,
                    CreatedOn = DateTime.UtcNow
                };

                _logger.LogInformation("Sending request to create a new supplier.");

                await _supplierRepo.Create(supplierToCreate);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the supplier.");
                return false;
            }
        }

        public async Task<bool> UpdateUser(AddSupplierViewModel supplierViewModel)
        {
            if (supplierViewModel == null)
            {
                throw new ArgumentNullException(nameof(supplierViewModel));
            }

            try
            {

                var supplierToUpdate = new Suppliers
                {
                    Id = supplierViewModel.Id,
                    ShipmentMarks = supplierViewModel.ShipmentMark,
                    SupplierName = supplierViewModel.SupplierName,
                    ContactPerson = supplierViewModel.ContactName,
                    Phone = supplierViewModel.PhoneNumber,
                    Email = supplierViewModel.Email,
                    Address = supplierViewModel.Address,
                    IsActive = true,
                    UpdatedOn = DateTime.UtcNow
                };

                _logger.LogInformation("Sending request to update an supplier.");

                await _supplierRepo.Update(supplierToUpdate);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the supplier.");
                return false;
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }

            try
            {

                _logger.LogInformation("Sending request to delete an supplier.");

                await _supplierRepo.Delete(id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the supplier.");
                return false;
            }
        }
    }
}
