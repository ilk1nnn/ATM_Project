﻿using ATM_Project.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Project.DataAccess.Abstractions
{
    public interface IUserRepository : IRepository<User>
    {
    }
}
