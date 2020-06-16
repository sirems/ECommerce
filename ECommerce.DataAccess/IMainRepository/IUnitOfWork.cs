using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.DataAccess.IMainRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository category { get; }
        IProductRepository Product { get; }
        ICoverTypeRepository CoverType { get; }
        ISPCallRepository sp_call { get; }


        void Save();
    }
}
