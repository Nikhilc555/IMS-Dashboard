using IMS_Dashboard.Models.Entities;
using IMS_Dashboard.ViewModels.CategoryVM;
using IMS_Dashboard.ViewModels.SuppliersVM;
using IMS_Dashboard.ViewModels.UserVM;

namespace IMS_Dashboard.Services.SupplierService.Interface
{
    public interface IsupplierService
    {
        Task<IEnumerable<DisplaySupplierViewModel>> GetAllSuppliers();
        Task<DisplaySupplierViewModel> GetSupplierById(int id);
        Task<bool> CreateUser(AddSupplierViewModel userViewModel);
        Task<bool> UpdateUser(AddSupplierViewModel userViewModel);
        Task<bool> DeleteUser(int id);
    }
}
