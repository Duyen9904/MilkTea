using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PRN222.Assignment.Blazor.Client.Components.Pages
{
    public class LogoutBase : ComponentBase
    {
        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected IHostEnvironmentAuthenticationStateProvider HostAuthentication { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LogoutUser();
        }

        protected async Task LogoutUser()
        {
            // Create an empty/anonymous principal
            var anonymousPrincipal = new ClaimsPrincipal(new ClaimsIdentity());

            // Set the authentication state to anonymous
            HostAuthentication.SetAuthenticationState(
                Task.FromResult(new AuthenticationState(anonymousPrincipal)));

            // Redirect to login page
            NavigationManager.NavigateTo("/login");
        }
    }
}