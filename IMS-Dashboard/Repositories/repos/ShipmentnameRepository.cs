using IMS_Dashboard.Models.Entities;
using IMS_Dashboard.Repositories.interfaces;
using System.Security.Claims;

namespace IMS_Dashboard.Repositories.repos
{
    public class ShipmentnameRepository : IshipmentnameRepository
    {
        private readonly ImsDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShipmentnameRepository(ImsDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ShipmentList> GetByShipmentName(string name)
        {

            var shipment = _context.ShipmentList.FirstOrDefault(c => c.ShipmentName == name);
            return shipment;
        }
        public async Task Create(ShipmentList shipname)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                shipname.CreatedBy = Convert.ToInt32(userId);
                shipname.IsActive = true;

                _context.ShipmentList.Add(shipname);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<ShipmentList> Get(int? Id)
        {
            var shipmentname = _context.ShipmentList.FirstOrDefault(c => c.Id == Id);
            return shipmentname;
        }

        public async Task<IEnumerable<ShipmentList>> GetAll()
        {
            var shipmentlist = _context.ShipmentList.ToList().Where(u => u.IsActive);
            return shipmentlist;
        }

        public async Task<int> GetCountWithShipmentName(int id)
        {
            var shipmentlist = _context.ImportInventories.ToList().Where(u => u.Shipping_ref == id.ToString()).ToList().Count();
            return shipmentlist;
        }
    }
}
