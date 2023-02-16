namespace AquaPlayground
{
    using AquaPlayground.Repository;
    using AquaPlayground.Repository.Interfaces;
    using Microsoft.EntityFrameworkCore.Storage;

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AquaPlaygroundContext _context;
        private bool _disposed = false;

        private IProductRepository _productRepository;

        private ITagRepository _tagRepository;

        private IProductTypeRepository _productTypeRepository;

        private IManufacturerRepository _manufacturerRepository;

        private IUserRepository _userRepository;

        private IFavoriteProductRepository _favoriteProductRepository;

        private IProductToTagRepository _productToTagRepository;

        private ICartProductRepository _cartProductRepository;

        private IOrderRepository _orderRepository;

        public UnitOfWork(AquaPlaygroundContext context)
        {
            _context = context;
        }

        public IOrderRepository OrderRepository
        {
            get
            {
                if (_orderRepository is null)
                {
                    _orderRepository = new OrderRepository(_context);
                }

                return _orderRepository;
            }
        }

        public ICartProductRepository CartProductRepository
        {
            get
            {
                if (_cartProductRepository is null)
                {
                    _cartProductRepository = new CartProductRepository(_context);
                }

                return _cartProductRepository;
            }
        }

        public IProductToTagRepository ProductToTagRepository
        {
            get
            {
                if (_productToTagRepository is null)
                {
                    _productToTagRepository = new ProductToTagRepository(_context);
                }

                return _productToTagRepository;
            }
        }

        public IFavoriteProductRepository FavoriteProductRepository
        {
            get
            {
                if (_favoriteProductRepository is null)
                {
                    _favoriteProductRepository = new FavoriteProductRepository(_context);
                }

                return _favoriteProductRepository;
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository is null)
                {
                    _productRepository = new ProductRepository(_context);
                }

                return _productRepository;
            }
        }

        public ITagRepository TagRepository
        {
            get
            {
                if (_tagRepository is null)
                {
                    _tagRepository = new TagRepository(_context);
                }

                return _tagRepository;
            }
        }

        public IProductTypeRepository ProductTypeRepository
        {
            get
            {
                if (_productTypeRepository is null)
                {
                    _productTypeRepository = new ProductTypeRepository(_context);
                }

                return _productTypeRepository;
            }
        }

        public IManufacturerRepository ManufacturerRepository
        {
            get
            {
                if (_manufacturerRepository is null)
                {
                    _manufacturerRepository = new ManufacturerRepository(_context);
                }

                return _manufacturerRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository is null)
                {
                    _userRepository = new UserRepository(_context);
                }

                return _userRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public IDbContextTransaction CreateTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }
    }
}
