using IMS_Dashboard.Models.Entities;
using IMS_Dashboard.ViewModels.CategoryVM;

namespace IMS_Dashboard.Services.SupplierService.Interface
{
    public interface IsupplierService
    {
        Task<IEnumerable<Suppliers>> GetAllSuppliers();
    }
}
