namespace AquaPlayground.Repository
{
    using AquaPlayground.Models;
    using AquaPlayground.Repository.Interfaces;

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(AquaPlaygroundContext context)
            : base(context)
        {
        }
    }
}
