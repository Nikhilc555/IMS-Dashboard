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
            var query = "SELECT * FROM Category WHERE id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var category = await connection.QuerySingleOrDefaultAsync<Category>(query, new { Id });
                return category;
            }
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            var query = "SELECT * FROM Category";
            using (var connection = _context.CreateConnection())
            {
                var category = await connection.QueryAsync<Category>(query);
                return category;
            }
        }
    }
}
