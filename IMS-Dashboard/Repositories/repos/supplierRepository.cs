using IMS_Dashboard.Models.Entities;
using IMS_Dashboard.Repositories.interfaces;
using System.Security.Claims;

namespace IMS_Dashboard.Repositories.repos
{
    public class supplierRepository : IsupplierRepository
    {
        private readonly ImsDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public supplierRepository(ImsDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

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

        public async Task Create(Suppliers supplier)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                supplier.CreatedBy = Convert.ToInt32(userId);
                supplier.IsActive = true;

                _context.Suppliers.Add(supplier);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task Delete(int id)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var existingSupplier = await _context.Suppliers.FindAsync(id);

            if (existingSupplier != null)
            {
                // Update properties
                existingSupplier.IsActive = false;
                existingSupplier.UpdatedOn = DateTime.UtcNow;
                existingSupplier.UpdatedBy = Convert.ToInt32(userId);

                _context.Suppliers.Update(existingSupplier);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Update(Suppliers supplier)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var existingSupplier = await _context.Suppliers.FindAsync(supplier.Id);

            if (existingSupplier != null)
            {
                // Update properties
                existingSupplier.ShipmentMarks = supplier.ShipmentMarks;
                existingSupplier.SupplierName = supplier.SupplierName;
                existingSupplier.ContactPerson = supplier.ContactPerson;
                existingSupplier.Phone = supplier.Phone;
                existingSupplier.Email = supplier.Email;
                existingSupplier.IsActive = true;
                existingSupplier.UpdatedOn = DateTime.UtcNow;
                existingSupplier.UpdatedBy = Convert.ToInt32(userId);

                _context.Suppliers.Update(existingSupplier);
                await _context.SaveChangesAsync();
            }
        }
    }
}
