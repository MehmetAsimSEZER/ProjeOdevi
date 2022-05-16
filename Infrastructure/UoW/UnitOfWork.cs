using Domain.Repositories;
using Domain.UoW;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;


        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext ?? throw new ArgumentNullException($"{nameof(appDbContext)} can't be a null");
        }


        private ICategoryRepository _categoryRepository;
        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new CategoryRepository(_appDbContext);

                return _categoryRepository;
            }
        }

        private IUserRepository _userRepository;
        public IUserRepository UserRepository
        {
            get 
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_appDbContext);
                return _userRepository; 
            }
        }

        private IProductRepository _productRepository;
        public IProductRepository ProductRepository
        {
            get 
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(_appDbContext);
                return _productRepository; 
            }
        }

        private IPropertyRepository _propertyRepository;
        public IPropertyRepository PropertyRepository
        {
            get 
            { 
                if(_propertyRepository == null)
                    _propertyRepository = new PropertyRepository(_appDbContext);
                return _propertyRepository; 
            }
        }

        private IProductPropertyRepository _productPropertyRepository;
        public IProductPropertyRepository ProductPropertyRepository
        {
            get 
            {
                if(_productPropertyRepository == null)
                    _productPropertyRepository = new ProductPropertyRepository(_appDbContext);
                return _productPropertyRepository; 
            }
        }

        private IParentCategoryRepository _parentCategoryRepository;

        public IParentCategoryRepository ParentCategoryRepository
        {
            get 
            {
                if (_parentCategoryRepository == null)
                    _parentCategoryRepository = new ParentCategoryRepository(_appDbContext);
                return _parentCategoryRepository; 
            }
        }

        public async Task Commit()
        {
            await _appDbContext.SaveChangesAsync();
        }

        private bool _isDisposed = false;

        public async ValueTask DisposeAsync()
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                await DisposeAsync(true);
                GC.SuppressFinalize(this);
            }
        }

        private async Task DisposeAsync(bool disposing)
        {
            if (disposing)
                await _appDbContext.DisposeAsync();
        }

        public async Task ExecuteSqlRaw(string sql, params object[] parameteres)
        {
            await _appDbContext.Database.ExecuteSqlRawAsync(sql, parameteres);
        }
    }
}
