using IMS_Dashboard.Models.Entities;
using IMS_Dashboard.Repositories.interfaces;

namespace IMS_Dashboard.Repositories.repos
{
    public class userRepository:IuserRepository
    {
        private readonly ImsDbContext _context;

        public userRepository(ImsDbContext context)
        {
            _context = context;
        }

        public async Task<SystemUser> Get(int? Id)
        {
            var user = _context.SystemUsers .FirstOrDefault(c => c.Id == Id);
            return user;
        }

        public async Task<IEnumerable<SystemUser>> GetAll()
        {
            var users = _context.SystemUsers.ToList();
            return users;
        }
    }
}
