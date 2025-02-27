namespace IMS_Dashboard.Repositories.interfaces
{
    public interface IproductRepository
    {
        Task<Product> Get(int? Id);
        Task<IEnumerable<Product>> GetAll();
    }
}
