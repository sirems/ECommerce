using System.Linq;
using ECommerce.DataAccess.Data;
using ECommerce.DataAccess.IMainRepository;
using ECommerce.Models.DbModels;
    
namespace ECommerce.DataAccess.MainRepository
{
    public class ShoppingCardRepository:Repository<ShoppingCart>,IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;
        public ShoppingCardRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
            
        public void Update(ShoppingCart shoppingCart)
        {
            _db.Update(shoppingCart);
        }

      
    }
}
