using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Models.DbModels;

namespace ECommerce.DataAccess.IMainRepository
{
    public interface ICoverTypeRepository:IRepository<CoverType>
    {
        void Update(CoverType category);
    }
}
        