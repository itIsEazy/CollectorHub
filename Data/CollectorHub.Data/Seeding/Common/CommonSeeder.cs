namespace CollectorHub.Data.Seeding.Common
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CollectorHub.Common;
    using CollectorHub.Data.Common.Repositories;
    using CollectorHub.Data.Models.Common;
    using CollectorHub.Data.Models.User;
    using CollectorHub.Services.Data.Administration;
    using Microsoft.AspNetCore.Identity;

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
            priceSortASC.Name = "Cheapest";

            var priceSortDESC = new Sorting();
            priceSortDESC.Name = "Expensivest";

            var dateCreatedASC = new Sorting();
            dateCreatedASC.Name = "Oldest";

            var dateCreatedDESC = new Sorting();
            dateCreatedDESC.Name = "Newest";

            var viewCountASC = new Sorting();
            viewCountASC.Name = "Less viewed";

            var viewCountDESC = new Sorting();
            viewCountDESC.Name = "Most viewed";

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
