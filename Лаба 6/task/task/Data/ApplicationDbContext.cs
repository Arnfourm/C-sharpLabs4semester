using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Identity;
using task.Models;

namespace task.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    //public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    //public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
