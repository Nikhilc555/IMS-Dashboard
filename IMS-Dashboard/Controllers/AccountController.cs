using IMS_Dashboard.Models.Entities;
using IMS_Dashboard.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using IMS_Dashboard.Services.InventoryServices.Interface;

namespace IMS_Dashboard.Controllers
{
    public class AccountController : Controller
    {
        private readonly ImsDbContext _context;
        private readonly PasswordHasher<SystemUser> _passwordHasher;
        Encryption enc = new Encryption();

        public AccountController(ImsDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<SystemUser>();
        }

        [IgnoreAntiforgeryToken]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.SystemUsers.FirstOrDefault(u => u.UserName == model.Username);
                if (user != null)
                {
                    byte[] salt = new byte[16];
                    using (var rng = RandomNumberGenerator.Create())
                    {
                        rng.GetBytes(salt);
                    }

                    // Hash Password using PBKDF2
                    string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: model.Password,
                        salt: salt,
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 1000,
                        numBytesRequested: 32));


                    //var result = _passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);
                    var result = model.Password == user.Password ? true : false;
                    //if (result == PasswordVerificationResult.Success)
                    if (result)
                    {
                        var role = _context.Roles.FirstOrDefault(c => c.Id == user.RoleId);
                        var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.GivenName, user.NameOfUser),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, role.Role1)
                    };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties();

                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError("", "Invalid email or password.");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","Account");
        }
    }
}
