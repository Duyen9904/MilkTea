using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using PRN222.Assignment.Services.Interfaces;
using PRN222.Assignment.Repositories.Entities;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace ProductStore.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAccountService _accountService;

        public LoginModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToPage("/Milktea/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Email and Password are required.";
                return Page();
            }


            var memberAccount = await _accountService.Login(Email, Password);

            // Check if account exists and password matches
            if (memberAccount == null || memberAccount.Password != Password)
            {
                ErrorMessage = "Invalid email or password!";
                ModelState.AddModelError(string.Empty, ErrorMessage);
                return Page();
            }

            // Ensure only Admin can log in
            if (memberAccount.Role != "ADMIN")
            {
                ErrorMessage = "You do not have permission to access this function!";
                ModelState.AddModelError(string.Empty, ErrorMessage);
                return Page();
            }

            // Create authentication claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, memberAccount.Email),
                new Claim(ClaimTypes.Role, memberAccount.Role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToPage("/Milktea/Index");
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Login");
        }
    }
}

