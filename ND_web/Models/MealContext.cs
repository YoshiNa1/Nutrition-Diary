using System;
using Microsoft.EntityFrameworkCore;

namespace ND_web.Models
{
    public class MealContext: DbContext
    {
        public DbSet<MealsBindingModel> Meals { get; set; }
        public DbSet<FoodBindingModel> Foods { get; set; }
        public MealContext(DbContextOptions<MealContext> options) : base(options) { }

    }
}
