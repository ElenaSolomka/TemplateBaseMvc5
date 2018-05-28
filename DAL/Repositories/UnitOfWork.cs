using System;
using System.Data.Entity.Infrastructure;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
using Presentation.DAL.EF;


//using Presentation.DAL.Entities;

namespace DAL.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private DatabaseContext _ctx;
        private bool disposed;
        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private IClientManager clientManager;


        public UnitOfWork()
        {
            _ctx = new DatabaseContext(DatabaseConnectionStrings.NameConnectionStringFirstDatabase);
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_ctx));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_ctx));
            clientManager = new ClientManager(_ctx);
        }

        public void Save()
        {
            _ctx.SaveChanges();
        }


        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _ctx.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public DbContextConfiguration ContextConfiguration
        {
            get { return _ctx.Configuration; }
        }
        public IRepository<T> GetRepository<T>() where T : class
        {
            return new RepositoryFactory<T>(_ctx).GetRepositoryInstance();
        }


        public ApplicationUserManager UserManager
        {
            get { return userManager; }
        }

        public IClientManager ClientManager
        {
            get { return clientManager; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return roleManager; }
        }
    }
}