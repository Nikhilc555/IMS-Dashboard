using IMS_Dashboard.Models.Entities;
using IMS_Dashboard.Repositories.interfaces;

namespace IMS_Dashboard.Repositories.repos
{
    public class supplierRepository : IsupplierRepository
    {
        private readonly ImsDbContext _context;

        public supplierRepository(ImsDbContext context)
        {
            _context = context;
        }
        public async Task<Suppliers> Get(int? Id)
        {
            var supplier = _context.Suppliers.FirstOrDefault(c => c.Id == Id);
            return supplier;
        }

        public async Task<IEnumerable<Suppliers>> GetAll()
        {
            var supplier = _context.Suppliers.ToList();
            return supplier;
        }
    }
}
