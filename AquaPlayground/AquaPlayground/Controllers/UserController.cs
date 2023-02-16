namespace AquaPlayground.Controllers
{
    using AquaPlayground.Models;
    using AquaPlayground.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = AppRoles.Admin)]
    public class UserController : Controller
    {
        private const int RecordsPerPage = 10;
        private const int StartPage = 1;

        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;

        public UserController(
            IUserService userService,
            UserManager<User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = StartPage, int count = RecordsPerPage)
        {
            var result = await _userService.GetUsersInfo(page, count);
            return View(result);
        }

        [HttpGet]
        [ActionName("Search")]
        public async Task<IActionResult> GetUsersBySubstring(string substring, int page = StartPage, int count = RecordsPerPage)
        {
            var result = await _userService.GetUsersInfoBySubstring(substring, page, count);
            return View("Index", result);
        }

        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> EditUserRole(long userId)
        {
            User user = await _userManager.FindByIdAsync(userId.ToString());

            if (user != null)
            {
                var result = await _userService.GetUserWithRolesAsync(user);
                return View(result);
            }

            return RedirectToAction("404", "Error");
        }

        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> EditUserRole(long userId, List<string> roles)
        {
            User user = await _userManager.FindByIdAsync(userId.ToString());

            if (user != null)
            {
                await _userService.EditUserRolesAsync(user, roles);

                return RedirectToAction("Index");
            }

            return RedirectToAction("404", "Error");
        }
    }
}
