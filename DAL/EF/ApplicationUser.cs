using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
    }
}