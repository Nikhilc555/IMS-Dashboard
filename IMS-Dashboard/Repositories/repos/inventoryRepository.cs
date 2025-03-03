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
            try
            {
                _context.ImportInventories.Add(inv);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<IEnumerable<ImportInventory>> GetAll()
        {
            var inventory = _context.ImportInventories.ToList();
            return inventory;
        }

        public async Task<IEnumerable<ImportInventory>> GetAllPending()
        {
            var inventory = _context.ImportInventories.Where(c => c.Export_status == false).ToList();
            return inventory;
        }

        public async Task<IEnumerable<ImportInventory>> GetAllWithDate(string from_date, string to_date)
        {
            var inventory = _context.ImportInventories.ToList().AsEnumerable();

            if (!string.IsNullOrEmpty(from_date) && !string.IsNullOrEmpty(to_date))
            {
                if (DateTime.TryParse(from_date, out DateTime fromDate) &&
                    DateTime.TryParse(to_date, out DateTime toDate))
                {
                    // Filter based on CreatedOn date
                    inventory = inventory.Where(i => i.CreatedOn >= fromDate && i.CreatedOn < toDate.AddDays(1));
                }
                else
                {
                    return null;
                }
            }

            return inventory;
        }

        public async Task<bool> UpdateForShipment(List<int> remainingIds)
        {
            try
            {
                var allInventory = _context.ImportInventories.ToList();

                foreach (var inventory in allInventory)
                {
                    if (remainingIds.Contains(inventory.Id))
                    {
                        inventory.Export_status = true; // Mark as shipped
                        _context.ImportInventories.Update(inventory);
                    }
                }

                _context.SaveChanges();

                return true;
            }
            catch(Exception ex) 
            { 
                return false;
            }
        }
    }
}
