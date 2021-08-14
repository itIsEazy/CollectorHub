namespace CollectorHub.Data.Seeding.Common
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CollectorHub.Data.Models.Common;

    public class CommonSeeder : ISeeder
    {
        public CommonSeeder()
        {
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Sortings.Any())
            {
                return;
            }
            else
            {
                await this.SeedSorting(dbContext);
            }
        }

        private async Task SeedSorting(ApplicationDbContext dbContext)
        {
            var priceSortASC = new Sorting();
            priceSortASC.Name = "Expensivest";

            var priceSortDESC = new Sorting();
            priceSortDESC.Name = "Cheapest";

            var dateCreatedASC = new Sorting();
            dateCreatedASC.Name = "Newest";

            var dateCreatedDESC = new Sorting();
            dateCreatedDESC.Name = "Oldest";

            var viewCountASC = new Sorting();
            viewCountASC.Name = "Most viewed";

            var viewCountDESC = new Sorting();
            viewCountDESC.Name = "Less viewed";

            await dbContext.Sortings.AddAsync(priceSortASC);
            await dbContext.Sortings.AddAsync(priceSortDESC);
            await dbContext.Sortings.AddAsync(dateCreatedASC);
            await dbContext.Sortings.AddAsync(dateCreatedDESC);
            await dbContext.Sortings.AddAsync(viewCountASC);
            await dbContext.Sortings.AddAsync(viewCountDESC);

            dbContext.SaveChanges();
        }
    }
}
