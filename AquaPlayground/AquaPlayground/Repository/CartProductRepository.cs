namespace AquaPlayground.Repository
{
    using AquaPlayground.Models;
    using AquaPlayground.Repository.Interfaces;

    public class CartProductRepository : RepositoryBase<CartProduct>, ICartProductRepository
    {
        public CartProductRepository(AquaPlaygroundContext context)
            : base(context)
        {
        }
    }
}
