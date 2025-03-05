using IMS_Dashboard.ViewModels.SuppliersVM;

namespace IMS_Dashboard.Services.SupplierServices.Interface
{
    public interface ISupplierService
    {
        Task<int> GetAllSuppliersCountAsync();
        Task<IEnumerable<DisplaySupplierViewModel>> GetAllSuppliers();
        Task<CreateSupplierViewModel> CreateSupplier(CreateSupplierViewModel supplierViewModel);
        Task<DisplaySupplierViewModel> GetSupplierById(int id);
        Task<bool> CreateSupplier(AddSupplierViewModel supplierViewModel);
        Task<bool> UpdateSupplier(AddSupplierViewModel supplierViewModel);
        Task<bool> DeleteSupplier(int id);
    }
}
