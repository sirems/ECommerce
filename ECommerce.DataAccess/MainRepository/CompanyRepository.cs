using System.Linq;
using ECommerce.DataAccess.Data;
using ECommerce.DataAccess.IMainRepository;
using ECommerce.Models.DbModels;

namespace ECommerce.DataAccess.MainRepository
{
    public class CompanyRepository:Repository<Company>,ICompanyRepository
    {
        private readonly ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
                
        public void Update(Company company)
        {
            _db.Update(company);
        }
    }
}
