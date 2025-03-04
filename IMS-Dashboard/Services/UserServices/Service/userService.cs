using IMS_Dashboard.Models.Entities;
using IMS_Dashboard.Repositories.interfaces;
using IMS_Dashboard.Services.InventoryServices.Service;
using IMS_Dashboard.Services.UserServices.Interface;
using IMS_Dashboard.ViewModels.InventoryVM;
using IMS_Dashboard.ViewModels.UserVM;
using Microsoft.Extensions.Caching.Memory;

namespace IMS_Dashboard.Services.UserServices.Service
{
    public class userService : IuserService
    {
        private readonly IuserRepository _userRepo;
        private readonly IroleRepository _roleRepo;
        private ILogger<userService> _logger;
        private IMemoryCache _cache;
        private const string cacheKey = "UserList";
        Encryption enc = new Encryption();
        public userService(IuserRepository userRepo, ILogger<userService> logger, IMemoryCache cache, IroleRepository roleRepo)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            _logger = logger;
            _cache = cache;
        }
        public async Task<bool> CreateUser(AddUserViewModel userViewModel)
        {
            if (userViewModel == null)
            {
                throw new ArgumentNullException(nameof(userViewModel));
            }

            try
            {
                var userToCreate = new SystemUser
                {
                    UserName = userViewModel.username,
                    Password = userViewModel.password,
                    NameOfUser = userViewModel.name,
                    IsActive = true,
                    RoleId = userViewModel.role_id,
                    CreatedOn = DateTime.UtcNow
                };

                _logger.LogInformation("Sending request to create a new user.");

                await _userRepo.Create(userToCreate);

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the user.");
                return false;
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }

            try
            {

                _logger.LogInformation("Sending request to delete an inventory.");

                await _userRepo.Delete(id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the inventory.");
                return false;
            }
        }

        public async Task<IEnumerable<DisplayUserViewModel>> GetAllUsers()
        {
            if (_cache.TryGetValue(cacheKey, out IEnumerable<DisplayUserViewModel> cachedInventory))
            {
                _logger.LogInformation("Returning cached user list.");
                return cachedInventory;
            }

            try
            {

                var users = await _userRepo.GetAll();

                IEnumerable<DisplayUserViewModel> userList = new List<DisplayUserViewModel>();
                if (users != null)
                {
                    var tempList = userList.ToList();
                    foreach (var u in users)
                    {
                        var role = await _roleRepo.Get(u.RoleId);
                        tempList.Add(new DisplayUserViewModel
                        {
                            id = u.Id,
                            user_name = u.UserName,
                            password = u.Password,
                            name = u?.NameOfUser,
                            role = role.Role1,
                            status = u.IsActive ? "Active" : "Inactive"
                        });

                    }
                    userList = tempList;
                }

                return userList;
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Network issue occurred while contacting the user API.");
                throw new InventoryServiceException("Network error occurred while fetching users.", httpEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in GetAllUsers");
                throw new InventoryServiceException("An unexpected error occurred while fetching users.", ex);
            }
        }

        public async Task<bool> UpdateUser(AddUserViewModel userViewModel)
        {
            if (userViewModel == null)
            {
                throw new ArgumentNullException(nameof(userViewModel));
            }

            try
            {

                var userToCreate = new SystemUser
                {
                    Id = userViewModel.id,
                    UserName = userViewModel.username,
                    Password = userViewModel.password,
                    NameOfUser = userViewModel.name,
                    IsActive = true,
                    RoleId = userViewModel.role_id,
                    UpdatedOn = DateTime.UtcNow
                };

                _logger.LogInformation("Sending request to update an user.");

                await _userRepo.Update(userToCreate);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the user.");
                return false;
            }
        }

        public async Task<AddUserViewModel> GetUserById(int id)
        {
            if (_cache.TryGetValue(cacheKey, out AddUserViewModel cachedInventory))
            {
                _logger.LogInformation("Returning cached inventory list.");
                return cachedInventory;
            }

            try
            {

                var users = await _userRepo.Get(id);

                AddUserViewModel user = new AddUserViewModel();
                if (users != null)
                {
                    user = new AddUserViewModel
                    {
                        id = users.Id,
                        name = users.NameOfUser,
                        username = users.UserName,
                        password = user.password,
                        role_id = users.RoleId
                    };

                }

                return user;
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Network issue occurred while contacting the Inventory API.");
                throw new InventoryServiceException("Network error occurred while fetching inventories.", httpEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in GetAllInventories");
                throw new InventoryServiceException("An unexpected error occurred while fetching inventories.", ex);
            }
        }

    }
}
