using IMS_Dashboard.Models.Entities;

namespace IMS_Dashboard.Repositories.interfaces
{
    public interface IsupplierRepository
    {

        Task<Suppliers> Get(int? Id);
        Task<IEnumerable<Suppliers>> GetAll();
    }
}
