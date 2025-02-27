namespace IMS_Dashboard.Repositories.interfaces
{
    public interface IcategoryRepository
    {
        Task<Category> Get(int? Id);
        Task<IEnumerable<Category>> GetAll();
    }
}
