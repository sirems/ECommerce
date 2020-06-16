using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Models.DbModels;

namespace ECommerce.DataAccess.IMainRepository
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        //void Update(ApplicationUser applicationUser);
    }   
}
                    