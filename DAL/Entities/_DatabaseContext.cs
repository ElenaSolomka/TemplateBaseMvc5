using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Presentation.DAL.EF;

//using Presentation.DAL.Entities;

namespace DAL
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser> //DbContext
    {
        static DatabaseContext()
        {
             Database.SetInitializer<DatabaseContext>(new DbInitializer());
        }

        //public DbSet<Phone> Phones { get; set; }
        //public DbSet<Order> Orders { get; set; }
        public DbSet<SomeInfo> SomeInfos { get; set; }
        public DbSet<ClientProfile> ClientProfiles { get; set; }

        public DatabaseContext(string connectionString)
            : base(connectionString)
        { 
        }
    }
}