using Domain.Models.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ShoppingCartRepository : BaseRepository<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
