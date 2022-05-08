using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UoW
{
    public interface IUnitOfWork :IAsyncDisposable
    {
        ICategoryRepository CategoryRepository { get; }

        IUserRepository UserRepository { get; }

        Task Commit();

        Task ExecuteSqlRaw(string sql, params object[] parameteres);
    }
}
