namespace AquaPlayground.Services.Interfaces
{
    using AquaPlayground.Models;

    public interface IOrderService
    {
        public Task<List<Order>> GetOrdersListAsync(long userId);
    }
}
