namespace AquaPlayground.Controllers
{
    using AquaPlayground.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class CartProductController : Controller
    {
        private readonly ICartProductService _cartProductService;

        public CartProductController(ICartProductService cartProductService)
        {
            _cartProductService = cartProductService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.GetUserId();
            var result = await _cartProductService.GetProductsFromCartAsync(userId);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(long productId, int count)
        {
            if (count > 0)
            {
                var userId = User.GetUserId();
                await _cartProductService.AddProductToCartAsync(userId, productId, count);
            }
            else
            {
                TempData["Error"] = "Count cannot be negative!";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(long cartProductId)
        {
            var userId = User.GetUserId();

            try
            {
                await _cartProductService.DeleteProductFromCartAsync(userId, cartProductId);
            }
            catch (MethodAccessException)
            {
                return RedirectToAction("403", "Error");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> BuyProductCart()
        {
            var userId = User.GetUserId();
            await _cartProductService.BuyProductCartAsync(userId);
            return RedirectToAction("Index");
        }
    }
}
