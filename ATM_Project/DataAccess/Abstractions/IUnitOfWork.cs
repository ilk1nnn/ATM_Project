using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Project.DataAccess.Abstractions
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
    }
}
