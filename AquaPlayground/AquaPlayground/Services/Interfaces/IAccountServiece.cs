namespace AquaPlayground.Services.Interfaces
{
    using AquaPlayground.Models;
    using AquaPlayground.ViewModels;
    using Microsoft.AspNetCore.Identity;

    public interface IAccountServiece
    {
        public Task ForgotPasswordAsync(User user);

        public bool IsForgotPasswordCooldownOver(User user);

        public Task<IdentityResult> ResetPasswordAsync(ResetPasswordViewModel model);

        public Task<IdentityResult> RegisterAsync(RegisterViewModel model);
    }
}
