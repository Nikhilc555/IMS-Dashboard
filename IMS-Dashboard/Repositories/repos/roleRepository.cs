using IMS_Dashboard.Models.Entities;
using IMS_Dashboard.Repositories.interfaces;

namespace IMS_Dashboard.Repositories.repos
{
    public class roleRepository : IroleRepository
    {
        private readonly ImsDbContext _context;
        public roleRepository(ImsDbContext context)
        {
            _context = context;
        }
        public async Task<Role> Get(int? Id)
        {
            var role = _context.Roles.FirstOrDefault(c => c.Id == Id);
            return role;
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            var roles = _context.Roles.ToList();
            return roles;
        }
    }
}
