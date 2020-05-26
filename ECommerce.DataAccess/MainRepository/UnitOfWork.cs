using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.DataAccess.Data;
using ECommerce.DataAccess.IMainRepository;

namespace ECommerce.DataAccess.MainRepository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            category=new CategoryRepository(_db);
            sp_call=new SPCallRepository(_db);
        }
        public void Dispose()
        {
            _db.Dispose();
            
        }

        public ICategoryRepository category { get; private set; }
        public ISPCallRepository sp_call { get; private set; }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
