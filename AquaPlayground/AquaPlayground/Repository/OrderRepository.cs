namespace AquaPlayground.Repository
{
    using AquaPlayground.Models;
    using AquaPlayground.Repository.Interfaces;

    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(AquaPlaygroundContext context)
            : base(context)
        {
        }
    }
}
