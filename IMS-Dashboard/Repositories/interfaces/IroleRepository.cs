using IMS_Dashboard.Models.Entities;

namespace IMS_Dashboard.Repositories.interfaces
{
    public interface IroleRepository
    {
        Task<Role> Get(int? Id);
        Task<IEnumerable<Role>> GetAll();
    }
}
