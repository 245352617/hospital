using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.Recipe.EntityFrameworkCore
{
    public class RecipeHttpApiHostMigrationsDbContext : AbpDbContext<RecipeHttpApiHostMigrationsDbContext>
    {
        public RecipeHttpApiHostMigrationsDbContext(DbContextOptions<RecipeHttpApiHostMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureRecipe();
        }
    }
}
