using IMS_Dashboard.Models.Entities;

namespace IMS_Dashboard.Repositories.interfaces
{
    public interface IuserRepository
    {
        Task<SystemUser> Get(int? Id);
        Task<IEnumerable<SystemUser>> GetAll();
        Task<int> GetActiveUserCount();
        Task Create(SystemUser user);
        Task Update(SystemUser inv);
        Task Delete(int id);
    }
}
