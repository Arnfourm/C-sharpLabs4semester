using Microsoft.AspNetCore.Identity;

namespace task.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }
}
