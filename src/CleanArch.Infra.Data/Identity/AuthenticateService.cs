using CleanArch.Domain.Acount;
using Microsoft.AspNetCore.Identity;

namespace CleanArch.Infra.Data.Identity
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticateService(UserManager<ApplicationUser> userManager,
                                   SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
            return result.Succeeded;
        }

        public async Task<bool> RegisterUser(string email, string password)
        {
            var applicationUser = new ApplicationUser { UserName = email, Email = password };

            var result = _userManager.CreateAsync(applicationUser, password);
            if (result.IsCompletedSuccessfully)
                await _signInManager.SignInAsync(applicationUser, isPersistent: false);

            return result.IsCompletedSuccessfully;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

    }
}
