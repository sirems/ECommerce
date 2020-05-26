using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Models.DbModels;

namespace ECommerce.DataAccess.IMainRepository
{
    public interface ICategoryRepository:IRepository<Category>
    {
        void Update(Category category);
    }
}
