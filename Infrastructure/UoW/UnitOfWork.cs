
using Domain.Repositories;
using Domain.UoW;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
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
