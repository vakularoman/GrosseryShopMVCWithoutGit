namespace AquaPlayground.Controllers
{
    using System.Threading.Tasks;
    using AquaPlayground.Models;
    using AquaPlayground.Services.Interfaces;
    using AquaPlayground.ViewModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAccountServiece _accountServiece;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IAccountServiece accountServiece)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountServiece = accountServiece;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!(await IsRegisterModelValid(model)))
            {
                return View(model);
            }

            var result = await _accountServiece.RegisterAsync(model);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Product");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            var user = await _userManager.FindByNameAsync(loginVM.Name);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Product");
                    }
                }
            }

            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(loginVM);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return RedirectToAction("403", "Error");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
            {
                TempData["Error"] = "This email address does not exist.";
            }
            else if (!_accountServiece.IsForgotPasswordCooldownOver(user))
            {
                TempData["Error"] = "Email already sent.";
            }
            else
            {
                await _accountServiece.ForgotPasswordAsync(user);
                model.IsEmailSent = true;
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPassword(string uid, string token)
        {
            ResetPasswordViewModel resetPasswordModel = new ResetPasswordViewModel
            {
                Token = token,
                UserId = uid,
            };

            return View(resetPasswordModel);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountServiece.ResetPasswordAsync(model);

                if (result.Succeeded)
                {
                    model.IsCompleted = true;
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

        private async Task<bool> IsRegisterModelValid(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }

            var userMailExist = await _userManager.FindByEmailAsync(model.Email);
            if (userMailExist != null)
            {
                TempData["Error"] = "This email address is already in use";
                return false;
            }

            var userExist = await _userManager.FindByNameAsync(model.UserName);
            if (userExist != null)
            {
                TempData["Error"] = "This username is already in use";
                return false;
            }

            return true;
        }
    }
}
