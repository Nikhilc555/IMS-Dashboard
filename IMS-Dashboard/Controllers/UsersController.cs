using IMS_Dashboard.Repositories.interfaces;
using IMS_Dashboard.Repositories.repos;
using IMS_Dashboard.Services.InventoryServices.Interface;
using IMS_Dashboard.Services.InventoryServices.Service;
using IMS_Dashboard.Services.UserServices.Interface;
using IMS_Dashboard.ViewModels.InventoryVM;
using IMS_Dashboard.ViewModels.UserVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IMS_Dashboard.Controllers
{
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IuserService _userService;
        private readonly IroleRepository _roleRepository;

        public UsersController(ILogger<UsersController> logger, IuserService userService, IroleRepository roleRepository)
        {
            _logger = logger;
            _userService = userService;
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                _logger.LogInformation("Fetching all users");

                var users = await _userService.GetAllUsers();
                return View(users);
            }
            catch (HttpRequestException ex)
            {
                // Log Exception
                _logger.LogError(ex, "Error fetching users from the API.");
                throw;
            }
        }

        [HttpGet("AddUser")]
        public async Task<IActionResult> AddUser()
        {
            var userViewModel = new AddUserViewModel();

            // Fetch dropdowns from the API
            await PopulateDropDownsNew(userViewModel);
            return View(userViewModel);
        }

        [HttpGet("EditUser")]
        public async Task<IActionResult> EditUser(int id)
        {
            var addUserViewModel = new AddUserViewModel();
                
            var user = await _userService.GetUserById(id);

            addUserViewModel.id = user.id;
            addUserViewModel.name = user.name;
            addUserViewModel.username = user.username;
            addUserViewModel.password = user.password;
            addUserViewModel.role_id = user.role_id;

            // Fetch dropdowns from the API
            await PopulateDropDownsNew(addUserViewModel);
            return View(addUserViewModel);
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(AddUserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors);
                foreach (var error in errors)
                {
                    _logger.LogError($"Model validation error: {error.ErrorMessage}");
                }

                await PopulateDropDownsNew(userViewModel);
                return View(userViewModel);
            }

            try
            {
                _logger.LogInformation("Attempting to create a new user.");
                bool result = await _userService.CreateUser(userViewModel);

                if (result)
                {
                    _logger.LogInformation("user creation successful. Redirecting to GetAllUsers.");
                    return RedirectToAction(nameof(GetAllUsers));
                }
                else
                {
                    _logger.LogWarning("user creation failed.");
                    ModelState.AddModelError("", "An error occurred while creating the user.");
                    await PopulateDropDownsNew(userViewModel);
                    return View(userViewModel);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the user.");
                ModelState.AddModelError("", "An error occurred while creating the user.");
                await PopulateDropDownsNew(userViewModel);
                return View(userViewModel);
            }
        }

        [HttpPost("EditUser")]
        public async Task<IActionResult> EditUser(AddUserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors);
                foreach (var error in errors)
                {
                    _logger.LogError($"Model validation error: {error.ErrorMessage}");
                }

                await PopulateDropDownsNew(userViewModel);
                return View(userViewModel);
            }

            try
            {
                _logger.LogInformation("Attempting to create a new user.");
                bool result = await _userService.UpdateUser(userViewModel);

                if (result)
                {
                    _logger.LogInformation("user creation successful. Redirecting to GetAllUsers.");
                    return RedirectToAction(nameof(GetAllUsers));
                }
                else
                {
                    _logger.LogWarning("user creation failed.");
                    ModelState.AddModelError("", "An error occurred while creating the user.");
                    await PopulateDropDownsNew(userViewModel);
                    return View(userViewModel);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the user.");
                ModelState.AddModelError("", "An error occurred while creating the user.");
                await PopulateDropDownsNew(userViewModel);
                return View(userViewModel);
            }
        }

        [HttpPost("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {

            try
            {
                _logger.LogInformation("Attempting to delete an user.");
                bool result = await _userService.DeleteUser(id);

                if (result)
                {
                    _logger.LogInformation("User deletion successful. Redirecting to GetAllUsers.");
                    return RedirectToAction(nameof(GetAllUsers));
                }
                else
                {
                    _logger.LogWarning("User deletion failed.");
                    ModelState.AddModelError("", "An error occurred while deleting the user.");
                    return RedirectToAction(nameof(GetAllUsers));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the user.");
                ModelState.AddModelError("", "An error occurred while deleting the user.");
                return RedirectToAction(nameof(GetAllUsers));
            }
        }
        public IActionResult Index()
        {
            return View();
        }
        private async Task PopulateDropDownsNew(AddUserViewModel userViewModel)
        {
            var roles = await _roleRepository.GetAll();
            userViewModel.Roles = roles.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Role1
            }).ToList();
        }
    }
}
