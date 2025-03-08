using Humanizer;
using IMS_Dashboard.Models.Entities;
using IMS_Dashboard.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IMS_Dashboard.Repositories.repos
{
    public class inventoryRepository : IinventoryRepository
    {
        private readonly ImsDbContext _context; 
        private readonly IHttpContextAccessor _httpContextAccessor;
        public inventoryRepository(ImsDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task Create(ImportInventory inv)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                inv.CreatedBy = Convert.ToInt32(userId);
                inv.IsActive = "Y";

                _context.ImportInventories.Add(inv);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }
        public async Task Update(ImportInventory inv)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var existingInv = await _context.ImportInventories.FindAsync(inv.Id);

                if (existingInv != null)
                {
                    // Update properties
                    existingInv.CtnNo = inv.CtnNo;
                    //existingInv.ShippingMark = inv.ShippingMark;
                    existingInv.SupplierId = inv.SupplierId;
                    existingInv.Description = inv.Description;
                    existingInv.ProductId = inv.ProductId;
                    existingInv.Qty = inv.Qty;
                    existingInv.Weight = inv.Weight;
                    existingInv.Updated_by = Convert.ToInt32(userId);
                    existingInv.Updated_on = DateTime.UtcNow; // Optional timestamp update

                    _context.ImportInventories.Update(existingInv);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public async Task Delete(int id)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var existingInv = await _context.ImportInventories.FindAsync(id);

                if (existingInv != null)
                {
                    // Update properties
                    existingInv.IsActive = "N";
                    existingInv.Updated_by = Convert.ToInt32(userId);
                    existingInv.Updated_on = DateTime.UtcNow; // Optional timestamp update

                    _context.ImportInventories.Update(existingInv);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<IEnumerable<ImportInventory>> GetAll()
        {
            var inventory = _context.ImportInventories.Where(i => i.IsActive == "Y").ToList();
            return inventory;
        }

        public async Task<ImportInventory> GetById(int id)
        {
            var inventory = _context.ImportInventories.FirstOrDefault(c => c.Id == id);
            return inventory;
        }

        public async Task<IEnumerable<ImportInventory>> GetAllPending()
        {
            var inventory = _context.ImportInventories.Where(c => c.Export_status == false && c.IsActive == "Y").ToList();
            return inventory;
        }

        public async Task<IEnumerable<ImportInventory>> GetAllWithDate(string from_date, string to_date)
        {
            var inventory = _context.ImportInventories.ToList().AsEnumerable();

            if (!string.IsNullOrEmpty(from_date) && !string.IsNullOrEmpty(to_date))
            {
                DateTime fromDate, toDate;

                // Ensure from_date and to_date are properly handled
                if (!DateTime.TryParse(from_date as string, out fromDate))
                {
                    fromDate = DateTime.Today;
                }

                if (!DateTime.TryParse(to_date as string, out toDate))
                {
                    toDate = DateTime.Today;
                }
                // Filter based on CreatedOn date
                inventory = inventory.Where(i => i.CreatedOn >= fromDate && i.CreatedOn < toDate.AddDays(1) && i.IsActive != "N");
            }
            else
            {
                DateTime fromDate, toDate;

                // Ensure from_date and to_date are properly handled
                if (!DateTime.TryParse(from_date as string, out fromDate))
                {
                    fromDate = DateTime.Today;
                }

                if (!DateTime.TryParse(to_date as string, out toDate))
                {
                    toDate = DateTime.Today;
                }

                inventory = inventory.Where(i => i.CreatedOn >= fromDate && i.CreatedOn < toDate.AddDays(1) && i.IsActive != "N");
            }

            return inventory;
        }

        public async Task<bool> UpdateForShipment(List<int> remainingIds, string shippingRef)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userId != null)
                {
                    var allInventory = _context.ImportInventories.ToList();

                    foreach (var inventory in allInventory)
                    {
                        if (remainingIds.Contains(inventory.Id))
                        {
                            inventory.Export_status = true; // Mark as shipped
                            inventory.Shipping_ref = shippingRef;
                            inventory.Shipped_on = DateTime.Now;
                            inventory.Shipped_by = Convert.ToInt32(userId);
                            _context.ImportInventories.Update(inventory);
                        }
                    }

                    _context.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex) 
            { 
                return false;
            }
        }

        public async Task<IEnumerable<ImportInventory>> GetShipmentWithDate(string from_date, string to_date)
        {
            var inventory = _context.ImportInventories.ToList().AsEnumerable().Where(i => i.Export_status);

            if (!string.IsNullOrEmpty(from_date) && !string.IsNullOrEmpty(to_date))
            {
                DateTime fromDate, toDate;

                // Ensure from_date and to_date are properly handled
                if (!DateTime.TryParse(from_date as string, out fromDate))
                {
                    fromDate = DateTime.Today;
                }

                if (!DateTime.TryParse(to_date as string, out toDate))
                {
                    toDate = DateTime.Today;
                }
                // Filter based on CreatedOn date
                inventory = inventory.Where(i => i.Shipped_on >= fromDate && i.Shipped_on < toDate.AddDays(1) && i.IsActive != "N");
            }
            else
            {
                DateTime fromDate, toDate;

                // Ensure from_date and to_date are properly handled
                if (!DateTime.TryParse(from_date as string, out fromDate))
                {
                    fromDate = DateTime.Today;
                }

                if (!DateTime.TryParse(to_date as string, out toDate))
                {
                    toDate = DateTime.Today;
                }

                inventory = inventory.Where(i => i.Shipped_on >= fromDate && i.Shipped_on < toDate.AddDays(1) && i.IsActive != "N");
            }

            return inventory;
        }

        public Task<int> GetShippedInventoryCount()
        {
            var ship_inventory = _context.ImportInventories.ToList().Where(s => s.Export_status == true).Sum(s => s.Qty) ?? 0;
            return Task.FromResult(ship_inventory);
        }

        public Task<int> GetPendingInventoryCount()
        {
            var ship_inventory = _context.ImportInventories.ToList().Where(s => s.Export_status == false && s.IsActive == "Y").Sum(s => s.Qty) ?? 0;
            return Task.FromResult(ship_inventory);
        }

        public async Task<int> GetShipmentCountAsync()
        {
            return await _context.ImportInventories
                .Where(i => i.Export_status)
                .Select(i => i.Shipping_ref) // Select only shipping_ref
                .Distinct() // Get distinct values
                .CountAsync(); // Count them
        }

        public async Task<int> GetPendingShipmentCountAsync()
        {
            return await _context.ImportInventories
                .Where(i => !i.Export_status)
                .Select(i => i.Shipping_ref) // Select only shipping_ref
                .Distinct() // Get distinct values
                .CountAsync(); // Count them
        }
    }
}
