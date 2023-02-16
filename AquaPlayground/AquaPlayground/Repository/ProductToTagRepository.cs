namespace AquaPlayground.Repository
{
    using AquaPlayground.Models;
    using AquaPlayground.Repository.Interfaces;

    public class ProductToTagRepository : RepositoryBase<ProductToTag>, IProductToTagRepository
    {
        public ProductToTagRepository(AquaPlaygroundContext context)
            : base(context)
        {
        }
    }
}
