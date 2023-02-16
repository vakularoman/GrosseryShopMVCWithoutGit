namespace AquaPlayground.Services
{
    using AquaPlayground.Models;
    using AquaPlayground.Services.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Order>> GetOrdersListAsync(long userId)
        {
           return await _unitOfWork.OrderRepository.GetAsQueryable()
                .Where(x => x.UserId == userId)
                .Include(x => x.OrderProducts).ThenInclude(x => x.Product)
                .ToListAsync();
        }
    }
}
