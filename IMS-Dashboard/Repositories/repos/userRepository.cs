using IMS_Dashboard.Models.Entities;
using IMS_Dashboard.Repositories.interfaces;
using System.Security.Claims;
using IMS_Dashboard;

namespace IMS_Dashboard.Repositories.repos
{
    public class userRepository:IuserRepository
    {
        private readonly ImsDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public userRepository(ImsDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<SystemUser> Get(int? Id)
        {
            var user = _context.SystemUsers .FirstOrDefault(c => c.Id == Id);
            //user.Password = PasswordEncryption.Decrypt(user.Password);
            return user;
        }

        public async Task<IEnumerable<SystemUser>> GetAll()
        {
            var users = _context.SystemUsers.ToList().Where(u => u.IsActive);
            //foreach(var user in users)
            //{
            //    user.Password = PasswordEncryption.Decrypt(user.Password);
            //}
            return users;
        }
        public async Task Create(SystemUser user)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                user.CreatedBy = Convert.ToInt32(userId);
                user.Password = user.Password;
                user.IsActive = true;

                _context.SystemUsers.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task Update(SystemUser user)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var existingUser = await _context.SystemUsers.FindAsync(user.Id);

            if (existingUser != null)
            {
                // Update properties
                existingUser.NameOfUser = user.NameOfUser;
                existingUser.UserName = user.UserName;
                existingUser.Password = user.Password;
                existingUser.RoleId = user.RoleId;
                existingUser.IsActive = true;
                existingUser.UpdatedOn = DateTime.UtcNow;
                existingUser.UpdatedBy = Convert.ToInt32(userId);

                _context.SystemUsers.Update(existingUser);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var existingUser = await _context.SystemUsers.FindAsync(id);

                if (existingUser != null)
                {
                    // Update properties
                    existingUser.IsActive = false;
                    existingUser.UpdatedBy = Convert.ToInt32(userId);
                    existingUser.UpdatedOn = DateTime.UtcNow; // Optional timestamp update

                    _context.SystemUsers.Update(existingUser);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public Task<int> GetActiveUserCount()
        {
            var user = _context.SystemUsers.ToList().Where(s => s.IsActive == true).Count();
            return Task.FromResult(user);
        }
    }
}
