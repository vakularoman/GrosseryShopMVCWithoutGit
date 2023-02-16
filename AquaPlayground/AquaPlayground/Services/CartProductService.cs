namespace AquaPlayground.Services
{
    using AquaPlayground.Models;
    using AquaPlayground.Services.Interfaces;
    using AquaPlayground.ViewModels;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    public class CartProductService : ICartProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CartProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CartProductViewModel> GetProductsFromCartAsync(long userId)
        {
            var cartProduct = new CartProductViewModel();
            cartProduct.Products = await _unitOfWork.CartProductRepository
                .GetAsQueryable()
                .Where(x => x.UserId == userId)
                .Select(x => new SingleProductCartViewModel
                { CartProductId = x.CartProductId, Count = x.Count, ProductName = x.Product.Name, Price = x.Product.Price })
                .ToListAsync();

            cartProduct.PriceSum = Math.Round(cartProduct.Products.Sum(x => x.Price * x.Count), 2);

            return cartProduct;
        }

        public async Task AddProductToCartAsync(long userId, long productId, int count)
        {
            var oldProduct = await _unitOfWork.CartProductRepository.GetAsQueryable()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);

            if (oldProduct is null)
            {
                var newOrder = new CartProduct { UserId = userId, ProductId = productId, Count = count };
                await _unitOfWork.CartProductRepository.AddAsync(newOrder);
            }
            else
            {
                _unitOfWork.CartProductRepository.Update(oldProduct);
                oldProduct.Count += count;
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteProductFromCartAsync(long userId, long cartProductId)
        {
            var product = await _unitOfWork.CartProductRepository.GetAsQueryable()
                .FirstAsync(x => x.CartProductId == cartProductId);

            if (product.UserId != userId)
            {
                throw new MethodAccessException();
            }

            _unitOfWork.CartProductRepository.Remove(product);
            await _unitOfWork.SaveAsync();
        }

        public async Task BuyProductCartAsync(long userId)
        {
            var cartData = await _unitOfWork.CartProductRepository.GetAsQueryable()
                .Where(x => x.UserId == userId)
                .Select(x => new { Cart = x, Price = x.Product.Price })
                .ToListAsync();

            var cart = cartData.Select(x => x.Cart).ToList();

            var order = new Order
            {
                UserId = userId,
                CreationTime = DateTime.UtcNow,
                OrderProducts = _mapper.Map<List<CartProduct>, List<OrderProduct>>(cart),
                TotalSpent = cartData.Sum(x => x.Price * x.Cart.Count),
            };

            await _unitOfWork.OrderRepository.AddAsync(order);
            _unitOfWork.CartProductRepository.RemoveRange(cart);

            await _unitOfWork.SaveAsync();
        }
    }
}
