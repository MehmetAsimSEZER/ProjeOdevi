﻿using Domain.Repositories;
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

        IProductRepository ProductRepository { get; }

        IPropertyRepository PropertyRepository { get; }

        IProductPropertyRepository ProductPropertyRepository { get; }

        IParentCategoryRepository ParentCategoryRepository { get; }

        IShoppingCartRepository ShoppingCartRepository { get; }

        IProductCartRelRepository ProductCartRelRepository { get; }

        Task Commit();

    }
}
