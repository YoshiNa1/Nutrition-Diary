using Microsoft.EntityFrameworkCore;

namespace ND_web.Models
{
    public class FoodContext : DbContext
    {
        public DbSet<FoodBindingModel> Foods { get; set; }
        public FoodContext(DbContextOptions<FoodContext> options) : base (options) { }
    }
}
