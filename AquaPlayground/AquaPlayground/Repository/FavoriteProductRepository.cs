namespace AquaPlayground.Repository
{
    using AquaPlayground.Models;
    using AquaPlayground.Repository.Interfaces;

    public class FavoriteProductRepository : RepositoryBase<FavoriteProduct>, IFavoriteProductRepository
    {
        public FavoriteProductRepository(AquaPlaygroundContext context)
            : base(context)
        {
        }
    }
}
