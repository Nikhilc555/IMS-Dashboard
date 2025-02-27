using IMS_Dashboard.Models.Entities;
using IMS_Dashboard.Repositories.interfaces;

namespace IMS_Dashboard.Repositories.repos
{
    public class inventoryRepository : IinventoryRepository
    {
        private readonly ImsDbContext _context;
        public inventoryRepository(ImsDbContext context)
        {
            _context = context;
        }
        public async Task Create(ImportInventory inv)
        {
            _context.ImportInventories.Add(inv);
            await _context.SaveChangesAsync();
        }
    }
}
