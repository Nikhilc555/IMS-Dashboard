using IMS_Dashboard.Repositories.interfaces;
using Dapper;

namespace IMS_Dashboard.Repositories.repos
{
    public class categoryRepository : IcategoryRepository
    {
        private readonly ImsDbContext _context;
        public categoryRepository(ImsDbContext context)
        {
            _context = context;     
        }
        public async Task<Category> Get(int? Id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == Id);
            return category;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            var category = _context.Categories.ToList();
            return category;
        }
    }
}
