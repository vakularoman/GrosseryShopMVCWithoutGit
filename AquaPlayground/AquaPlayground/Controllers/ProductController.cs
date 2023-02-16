namespace AquaPlayground.Controllers
{
    using AquaPlayground.Models;
    using AquaPlayground.Services.Interfaces;
    using AquaPlayground.ViewModels;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    [Authorize]
    public class ProductController : Controller
    {
        private const int RecordsPerPage = 6;
        private const int StartPage = 1;

        private readonly IProductService _productService;
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IProductService productService, IUnitOfWork unitOfWork)
        {
            _productService = productService;
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = AppRoles.Editor)]
        public async Task<IActionResult> Create()
        {
            ViewBag.Manufacturers = new SelectList(await _unitOfWork.ManufacturerRepository.GetElementListAsync(), "ManufacturerId", "Name");
            ViewBag.Types = new SelectList(await _unitOfWork.ProductTypeRepository.GetElementListAsync(), "ProductTypeId", "Name");
            ViewBag.Tags = new MultiSelectList(await _unitOfWork.TagRepository.GetElementListAsync(), "TagId", "Name");

            return View();
        }

        [HttpPost]
        [Authorize(Roles = AppRoles.Editor)]
        public async Task<IActionResult> Create([FromForm]ProductPostViewModel product)
        {
            if (ModelState.IsValid)
            {
                var productId = await _productService.CreateProductAsync(product);
                return RedirectToAction("Details", new { id = productId } );
            }

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [ActionName("Details")]
        public async Task<IActionResult> GetProduct(long id)
        {
            var result = await _productService.GetProductByIdAsync(id);

            if (result is null)
            {
                return RedirectToAction("Error", "404");
            }

            return View(result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = StartPage, int count = RecordsPerPage)
        {
            var isAuthorized = User.TryGetUserId(out long userId);
            var result = isAuthorized
                ? await _productService.GetProductsByIdAsync(userId, page, count)
                : await _productService.GetProductsAsync(page, count);

            return View(result);
        }

        [HttpGet]
        [ActionName("Search")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductsByName(string substring, int page = StartPage, int count = RecordsPerPage)
        {
            var isAuthorized = User.TryGetUserId(out long userId);
            var result = isAuthorized ?
                await _productService.GetProductsBySubstringByIdAsync(substring, userId, page, count) :
                await _productService.GetProductsBySubstringAsync(substring, page, count);

            return View("Index", result);
        }

        [HttpGet]
        [ActionName("SearchByCategory")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductsByCategory(long id, int page = StartPage, int count = RecordsPerPage)
        {
            var isAuthorized = User.TryGetUserId(out long userId);
            var result = isAuthorized ?
                await _productService.GetProductsByCategoryByIdAsync(id, userId, page, count) :
                await _productService.GetProductsByCategoryAsync(id, page, count);

            return View("Index", result);
        }

        [HttpGet]
        [ActionName("SearchByType")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductsByType(long id, int page = StartPage, int count = RecordsPerPage)
        {
            var isAuthorized = User.TryGetUserId(out long userId);
            var result = isAuthorized ?
                await _productService.GetProductsByTypeByIdAsync(id, userId, page, count) :
                await _productService.GetProductsByTypeAsync(id, page, count);

            return View("Index", result);
        }

        [HttpGet]
        [ActionName("SearchByTag")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductsByTag(long id, int page = StartPage, int count = RecordsPerPage)
        {
            var isAuthorized = User.TryGetUserId(out long userId);
            var result = isAuthorized ?
                await _productService.GetProductsByTagByIdAsync(id, userId, page, count) :
                await _productService.GetProductsByTagAsync(id, page, count);

            return View("Index", result);
        }

        [HttpGet]
        [ActionName("Favorite")]
        public async Task<IActionResult> GetFollowedProducts(int page = StartPage, int count = RecordsPerPage)
        {
            var userId = User.GetUserId();
            var result = await _productService.GetFavoriteProductsAsync(userId, page, count);

            return View("Index", result);
        }

        [HttpPost]
        public async Task<IActionResult> FollowProduct(long id)
        {
            var userId = User.GetUserId();
            var isSuccess = await _productService.TryFollowProductAsync(id, userId);

            if (!isSuccess)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UnfollowProduct(long id)
        {
            var userId = User.GetUserId();
            var isSuccess = await _productService.TryUnfollowProductAsync(id, userId);

            if (!isSuccess)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
