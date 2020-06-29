using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.DataAccess.IMainRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; } 
        ICompanyRepository Company { get; }
        ICoverTypeRepository CoverType { get; }
        IShoppingCartRepository ShoppingCart { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailRepository OrderDetail { get; }
        IApplicationUserRepository ApplicationUser { get; } 
        ISPCallRepository sp_call { get; }
            

        void Save();
    }
}
        