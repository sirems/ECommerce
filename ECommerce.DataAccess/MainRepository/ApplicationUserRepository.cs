using System.Linq;
using ECommerce.DataAccess.Data;
using ECommerce.DataAccess.IMainRepository;
using ECommerce.Models.DbModels;

namespace ECommerce.DataAccess.MainRepository
{
    public class ApplicationUserRepository:Repository<ApplicationUser>,IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }       

    }
}
