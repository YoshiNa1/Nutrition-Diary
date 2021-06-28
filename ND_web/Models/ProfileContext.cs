using Microsoft.EntityFrameworkCore;

namespace ND_web.Models
{
    public class ProfileContext : DbContext
    {
        public DbSet<ProfileBindingModel> Profiles { get; set; }
        public ProfileContext(DbContextOptions<ProfileContext> options) : base(options) { }
    }
}
