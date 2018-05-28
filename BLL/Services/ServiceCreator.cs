using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Services;
using DAL.Repositories;
using Presentation.Business.Interfaces;

namespace Presentation.Business.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService()
        {
            return new UserService(new UnitOfWork());
        }

    }
}
