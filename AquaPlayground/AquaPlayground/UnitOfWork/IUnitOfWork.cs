namespace AquaPlayground
{
    using AquaPlayground.Repository.Interfaces;
    using Microsoft.EntityFrameworkCore.Storage;

    public interface IUnitOfWork
    {
        public IProductRepository ProductRepository { get; }

        public ITagRepository TagRepository { get; }

        public IProductTypeRepository ProductTypeRepository { get; }

        public IManufacturerRepository ManufacturerRepository { get; }

        public IUserRepository UserRepository { get; }

        public IFavoriteProductRepository FavoriteProductRepository { get; }

        public IProductToTagRepository ProductToTagRepository { get; }

        public ICartProductRepository CartProductRepository { get; }

        public IOrderRepository OrderRepository { get; }

        public void Save();

        public Task SaveAsync();

        public IDbContextTransaction CreateTransaction();
    }
}
