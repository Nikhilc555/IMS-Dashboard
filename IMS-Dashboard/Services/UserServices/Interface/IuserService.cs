using IMS_Dashboard.ViewModels.InventoryVM;
using IMS_Dashboard.ViewModels.UserVM;

namespace IMS_Dashboard.Services.UserServices.Interface
{
    public interface IuserService
    {
        Task<int> GetAllUserCountAsync();
        Task<IEnumerable<DisplayUserViewModel>> GetAllUsers();
        Task<AddUserViewModel> GetUserById(int id);
        Task<bool> CreateUser(AddUserViewModel userViewModel);
        Task<bool> UpdateUser(AddUserViewModel userViewModel);
        Task<bool> DeleteUser(int id);
    }
}
