using IMS_Dashboard.Models.Entities;

namespace IMS_Dashboard.Repositories.interfaces
{
    public interface IshipmentnameRepository
    {
        Task<ShipmentList> Get(int? Id);
        Task<ShipmentList> GetByShipmentName(string name);
        Task<IEnumerable<ShipmentList>> GetAll();
        Task Create(ShipmentList shipname);
        Task<int> GetCountWithShipmentName(int id);
        Task<IEnumerable<ShipmentList>> GetAllList();
    }
}
