namespace AquaPlayground.Services.Interfaces
{
    using AquaPlayground.Models;
    using AquaPlayground.ViewModels;

    public interface IProductService
    {
        public Task<long> CreateProductAsync(ProductPostViewModel productDto);

        public Task<Product> GetProductByIdAsync(long id);

        public Task<bool> TryFollowProductAsync(long productId, long userId);

        public Task<bool> TryUnfollowProductAsync(long productId, long userId);

        public Task<PagingListViewModel<ProductGetViewModel>> GetProductsAsync(int page, int count);

        public Task<PagingListViewModel<ProductGetViewModel>> GetProductsByIdAsync(long userId, int page, int count);

        public Task<PagingListViewModel<ProductGetViewModel>> GetProductsBySubstringAsync(string substring, int page, int count);

        public Task<PagingListViewModel<ProductGetViewModel>> GetProductsBySubstringByIdAsync(string substring, long userId, int page, int count);

        public Task<PagingListViewModel<ProductGetViewModel>> GetFavoriteProductsAsync(long userId, int page, int count);

        public Task<PagingListViewModel<ProductGetViewModel>> GetProductsByCategoryAsync(long categoryId, int page, int count);

        public Task<PagingListViewModel<ProductGetViewModel>> GetProductsByCategoryByIdAsync(long categoryId, long userId, int page, int count);

        public Task<PagingListViewModel<ProductGetViewModel>> GetProductsByTypeAsync(long typeId, int page, int count);

        public Task<PagingListViewModel<ProductGetViewModel>> GetProductsByTypeByIdAsync(long typeId, long userId, int page, int count);

        public Task<PagingListViewModel<ProductGetViewModel>> GetProductsByTagAsync(long tagId, int page, int count);

        public Task<PagingListViewModel<ProductGetViewModel>> GetProductsByTagByIdAsync(long tagId, long userId, int page, int count);
    }
}
