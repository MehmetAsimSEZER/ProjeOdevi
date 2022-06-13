using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{

    public class UserRepository : BaseRepository<AppUser>, IUserRepository
    {

        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }

    }
}
