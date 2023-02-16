namespace AquaPlayground.Services
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using AquaPlayground.Models;
    using AquaPlayground.Services.Interfaces;
    using AquaPlayground.ViewModels;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;

    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISavingImageService _imageService;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, ISavingImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageService = imageService;
        }

        public async Task<long> CreateProductAsync(ProductPostViewModel productDto)
        {
            var nutrition = _mapper.Map<Nutrition>(productDto);

            var product = _mapper.Map<Product>(productDto);
            product.ImagePath = await _imageService.SaveImage(productDto.Image);
            product.Nutrition = nutrition;

            await _unitOfWork.ProductRepository.AddAsync(product);

            await _unitOfWork.SaveAsync();

            return product.ProductId;
        }

        public async Task<PagingListViewModel<ProductGetViewModel>> GetProductsBySubstringAsync(string substring, int page, int count)
        {
            if (substring is not null && substring.Length > 0)
            {
                return await GetPagingListByCondition(page, count, x => x.Name.Contains(substring));
            }
            else
            {
                return await GetProductsAsync(page, count);
            }
        }

        public async Task<PagingListViewModel<ProductGetViewModel>> GetProductsBySubstringByIdAsync(string substring, long userId, int page, int count)
        {
            var result = await GetProductsBySubstringAsync(substring, page, count);
            await SetFavoriteProductsFlag(result.Elements, userId);

            return result;
        }

        public async Task<PagingListViewModel<ProductGetViewModel>> GetProductsAsync(int page, int count)
        {
            return await GetPagingListByCondition(page, count, x => true);
        }

        public async Task<PagingListViewModel<ProductGetViewModel>> GetProductsByIdAsync(long userId, int page, int count)
        {
            var result = await GetProductsAsync(page, count);
            await SetFavoriteProductsFlag(result.Elements, userId);

            return result;
        }

        public async Task<PagingListViewModel<ProductGetViewModel>> GetProductsByCategoryAsync(long categoryId, int page, int count)
        {
            return await GetPagingListByCondition(page, count, x => x.ProductType.ProductCategoryId == categoryId);
        }

        public async Task<PagingListViewModel<ProductGetViewModel>> GetProductsByCategoryByIdAsync(long categoryId, long userId, int page, int count)
        {
            var result = await GetProductsByCategoryAsync(categoryId, page, count);
            await SetFavoriteProductsFlag(result.Elements, userId);

            return result;
        }

        public async Task<PagingListViewModel<ProductGetViewModel>> GetProductsByTypeAsync(long typeId, int page, int count)
        {
            return await GetPagingListByCondition(page, count, x => x.ProductType.ProductTypeId == typeId);
        }

        public async Task<PagingListViewModel<ProductGetViewModel>> GetProductsByTypeByIdAsync(long typeId, long userId, int page, int count)
        {
            var result = await GetProductsByTypeAsync(typeId, page, count);
            await SetFavoriteProductsFlag(result.Elements, userId);

            return result;
        }

        public async Task<PagingListViewModel<ProductGetViewModel>> GetProductsByTagAsync(long tagId, int page, int count)
        {
            var products = await _unitOfWork.ProductToTagRepository.GetAsQueryable()
                .Where(x => x.TagId == tagId)
                .Skip((page - 1) * count)
                .Take(count)
                .Select(x => x.Product)
                .ToListAsync();

            var productModels = _mapper.Map<List<Product>, List<ProductGetViewModel>>(products);

            var result = new PagingListViewModel<ProductGetViewModel>();
            result.Elements = productModels;
            result.CurrentPageNumber = page;
            result.RecordsPerPageCount = count;
            result.TotalPagesCount = RoundPages(await _unitOfWork.ProductToTagRepository.CountAsync(x => x.TagId == tagId), count);

            return result;
        }

        public async Task<PagingListViewModel<ProductGetViewModel>> GetProductsByTagByIdAsync(long typeId, long userId, int page, int count)
        {
            var result = await GetProductsByTagAsync(typeId, page, count);
            await SetFavoriteProductsFlag(result.Elements, userId);

            return result;
        }

        public async Task<PagingListViewModel<ProductGetViewModel>> GetFavoriteProductsAsync(long userId, int page, int count)
        {
            var products = await _unitOfWork.FavoriteProductRepository.GetAsQueryable()
                .Where(x => x.UserId == userId)
                .Select(x => x.Product)
                .Skip((page - 1) * count)
                .Take(count)
                .ToListAsync();

            var productModels = _mapper.Map<List<Product>, List<ProductGetViewModel>>(products);

            var result = new PagingListViewModel<ProductGetViewModel>();
            result.Elements = productModels;
            result.CurrentPageNumber = page;
            result.RecordsPerPageCount = count;
            result.TotalPagesCount = RoundPages(await _unitOfWork.FavoriteProductRepository.CountAsync(x => x.UserId == userId), count);

            result.Elements.ForEach(x => x.IsFavourite = true);

            return result;
        }

        public async Task<Product> GetProductByIdAsync(long id)
        {
            return await _unitOfWork.ProductRepository
                .GetAsQueryable()
                .Include(x => x.Nutrition)
                .Include(x => x.Manufacturer)
                .Include(x => x.ProductType).ThenInclude(x => x.ProductCategory)
                .Include(x => x.ProductTags).ThenInclude(x => x.Tag)
                .FirstOrDefaultAsync(x => x.ProductId == id);
        }

        public async Task<bool> TryFollowProductAsync(long productId, long userId)
        {
            var sameRecord = await _unitOfWork.FavoriteProductRepository
                .GetAsQueryable()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);

            if (sameRecord is not null)
            {
                return false;
            }

            await _unitOfWork.FavoriteProductRepository
                .AddAsync(new FavoriteProduct { ProductId = productId, UserId = userId });
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> TryUnfollowProductAsync(long productId, long userId)
        {
            var product = await _unitOfWork.FavoriteProductRepository
                .GetAsQueryable()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);

            if (product is null)
            {
                return false;
            }

            _unitOfWork.FavoriteProductRepository.Remove(product);
            await _unitOfWork.SaveAsync();

            return true;
        }

        private async Task<PagingListViewModel<ProductGetViewModel>> GetPagingListByCondition(
            int page,
            int count,
            Expression<Func<Product, bool>> predicate)
        {
            var products = await _unitOfWork.ProductRepository.GetElementListAsync(predicate, page, count);

            var productModels = _mapper.Map<List<Product>, List<ProductGetViewModel>>(products);

            var result = new PagingListViewModel<ProductGetViewModel>();
            result.Elements = productModels;
            result.CurrentPageNumber = page;
            result.RecordsPerPageCount = count;
            result.TotalPagesCount = RoundPages(await _unitOfWork.ProductRepository.CountAsync(predicate), count);

            return result;
        }

        private async Task SetFavoriteProductsFlag(List<ProductGetViewModel> products, long userId)
        {
            var favoriteProducts = await _unitOfWork.FavoriteProductRepository.GetElementListAsync(x => x.UserId == userId);
            products.ForEach(x => x.IsFavourite = favoriteProducts.Any(y => y.ProductId == x.ProductId));
        }

        private int RoundPages(int allCount, int perPageCount)
        {
            return (int)Math.Ceiling((double)allCount / perPageCount);
        }
    }
}
