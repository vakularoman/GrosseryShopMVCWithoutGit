namespace AquaPlayground.Services
{
    using AquaPlayground.Models;
    using AquaPlayground.Services.Interfaces;
    using AquaPlayground.ViewModels;
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;

    public class AccountService : IAccountServiece
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AccountService(
            UserManager<User> userManager,
            IEmailService emailService,
            IUnitOfWork unitOfWork,
            IConfiguration config,
            IMapper mapper,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _unitOfWork = unitOfWork;
            _config = config;
            _mapper = mapper;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
        {
            using var transaction = _unitOfWork.CreateTransaction();

            var user = _mapper.Map<User>(model);
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, AppRoles.User);
                await _signInManager.SignInAsync(user, false);

                transaction.Commit();
            }

            return result;
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            model.Token = model.Token.Replace(' ', '+');

            var user = await _userManager.FindByIdAsync(model.UserId);
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

            return result;
        }

        public async Task ForgotPasswordAsync(User user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var domain = _config.GetValue<string>("Application:AppDomain");
            var path = _config.GetValue<string>("Application:ForgotPassword");

            var link = string.Format(domain + path, user.Id, token);

            await _emailService.SendResetPasswordEmailAsync(user.Email, link);

            _unitOfWork.UserRepository.Attach(user);
            user.ResetPasswordGenerationTime = DateTime.UtcNow;

            _unitOfWork.Save();
        }

        public bool IsForgotPasswordCooldownOver(User user)
        {
            if (user.ResetPasswordGenerationTime is not null)
            {
                var cooldown = _config.GetValue<int>("Application:ResetPasswordCooldown");
                return DateTime.UtcNow > user.ResetPasswordGenerationTime.GetValueOrDefault().AddSeconds(cooldown);
            }

            return true;
        }
    }
}
