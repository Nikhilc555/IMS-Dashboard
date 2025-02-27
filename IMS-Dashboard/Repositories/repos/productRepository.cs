using IMS_Dashboard.Repositories.interfaces;

namespace IMS_Dashboard.Repositories.repos
{
    public class productRepository : IproductRepository
    {
        private readonly ImsDbContext _context;

        public productRepository(ImsDbContext context)
        {
            _context = context;
        }
        public async Task<Product> Get(int? Id)
        {
            var category = _context.Products.FirstOrDefault(c => c.Id == Id);
            return category;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var category = _context.Products.ToList();
            return category;
        }
    }
}
