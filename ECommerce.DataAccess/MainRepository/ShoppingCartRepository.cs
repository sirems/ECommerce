using System.Linq;
using ECommerce.DataAccess.Data;
using ECommerce.DataAccess.IMainRepository;
using ECommerce.Models.DbModels;
    
namespace ECommerce.DataAccess.MainRepository
{
    public class ShoppingCartRepository:Repository<ShoppingCart>,IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;
        public ShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
            
        public void Update(ShoppingCart shoppingCart)
        {
            _db.Update(shoppingCart);
        }

      
    }
}
