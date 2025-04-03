using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PRN222.Assignment.Repositories.Entities;
using PRN222.Assignment.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PRN222.Assignment.Blazor.Client.Components.Pages
{
    public class LoginBase : ComponentBase
    {
        [Inject]
        protected IAccountService AccountService { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected IHostEnvironmentAuthenticationStateProvider HostAuthentication { get; set; }

        [Inject]
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected Account Account { get; set; } = new Account();
        protected bool RememberMe { get; set; } = false;
        protected bool IsLoading { get; set; } = false;
        protected string ErrorMessage { get; set; } = string.Empty;

        protected async Task HandleLogin()
        {
            ErrorMessage = string.Empty;
            IsLoading = true;

            try
            {
                var user = await AccountService.Login(Account.Email, Account.Password);

                if (user != null)
                {
                    if (user.Role.Equals("ADMIN", StringComparison.OrdinalIgnoreCase) ||
                        user.Role.Equals("STAFF", StringComparison.OrdinalIgnoreCase))
                    {
                        // Create the claims principal
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.AccountId.ToString()),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                            new Claim(ClaimTypes.Role, user.Role)
                        };

                        var identity = new ClaimsIdentity(claims, "MilkTeaShopAuthentication");
                        var principal = new ClaimsPrincipal(identity);

                        // Update the authentication state in the Blazor application
                        HostAuthentication.SetAuthenticationState(
                            Task.FromResult(new AuthenticationState(principal)));

                        // This is optional, but helpful to ensure the auth state is properly updated
                        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

                        // Navigate to index page
                        NavigationManager.NavigateTo("/index");
                        return;
                    }
                    else
                    {
                        ErrorMessage = "You do not have permission to access this application.";
                    }
                }
                else
                {
                    ErrorMessage = "Invalid email or password.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Login failed: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
                StateHasChanged();
            }
        }
    }
}