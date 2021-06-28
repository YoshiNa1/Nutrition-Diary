using Microsoft.EntityFrameworkCore;

namespace ND_web.Models
{
    public class RationContext : DbContext
    {
        public DbSet<RationBindingModel> Ration { get; set; }
        public RationContext(DbContextOptions<RationContext> options) : base(options) { }
    }
}
