using IMS_Dashboard.Models.Entities;
using IMS_Dashboard.Repositories.interfaces;
using IMS_Dashboard.Services.CategoryServices.Service;
using IMS_Dashboard.Services.SupplierService.Interface;
using IMS_Dashboard.ViewModels.CategoryVM;
using Microsoft.Extensions.Caching.Memory;
using static System.Net.WebRequestMethods;

namespace IMS_Dashboard.Services.SupplierService.Service
{
    public class supplierService : IsupplierService
    {
        private readonly IsupplierRepository _supplierRepo;
        private readonly IMemoryCache _cache;
        private readonly ILogger<CategoryService> _logger;
        private const string cacheKey = "SupplierList";
        public supplierService(IsupplierRepository supplierRepo)
        {
            _supplierRepo = supplierRepo;
        }
        public async Task<IEnumerable<Suppliers>> GetAllSuppliers()
        {
            if (_cache.TryGetValue(cacheKey, out IEnumerable<Suppliers> cachedSupplier))
            {
                _logger.LogInformation("Returning cached category list.");
                return cachedSupplier;
            }

            try
            {
                var categories = await _supplierRepo.GetAll();
                return categories.Select(c => new Suppliers
                {
                    Id = c.Id,
                    SupplierName = c.SupplierName
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
    }
}
