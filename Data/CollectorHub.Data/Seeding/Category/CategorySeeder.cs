namespace CollectorHub.Data.Seeding.Category
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CollectorHub.Common;
    using CollectorHub.Data.Models.Common;

    public class CategorySeeder : ISeeder
    {
        public CategorySeeder()
        {
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }
            else
            {
                await this.SeedCategories(dbContext);
            }
        }

        private async Task SeedCategories(ApplicationDbContext dbContext)
        {
            var legoCategory = new Category();

            legoCategory.Name = GlobalConstants.LegoCategoryName;
            legoCategory.ImageUrl = "https://logos-world.net/wp-content/uploads/2020/09/LEGO-Logo.png";

            var hotWheelsCategory = new Category();

            hotWheelsCategory.Name = GlobalConstants.HotWheelsCategoryName;
            hotWheelsCategory.ImageUrl = "https://w7.pngwing.com/pngs/281/543/png-transparent-hot-wheels-logo-hot-wheels-world-s-best-driver-car-logo-hot-wheels-hot-wheels-world-best.png";

            dbContext.Categories.Add(legoCategory);
            dbContext.Categories.Add(hotWheelsCategory);

            await dbContext.SaveChangesAsync();
        }
    }
}
