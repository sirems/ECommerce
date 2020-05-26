﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.DataAccess.IMainRepository
{
    public interface IUnitOfWork:IDisposable
    {
        ICategoryRepository category { get; }
        ISPCallRepository sp_call { get; }
        void Save();
    }
}
