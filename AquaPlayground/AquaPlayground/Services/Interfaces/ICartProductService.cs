namespace AquaPlayground.Services.Interfaces
{
    using AquaPlayground.ViewModels;

    public interface ICartProductService
    {
        public Task<CartProductViewModel> GetProductsFromCartAsync(long userId);

        public Task AddProductToCartAsync(long userId, long productId, int count);

        public Task DeleteProductFromCartAsync(long userId, long cartProductId);

        public Task BuyProductCartAsync(long userId);
    }
}
