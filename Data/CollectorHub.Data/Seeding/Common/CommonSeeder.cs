namespace CollectorHub.Data.Seeding.Common
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CollectorHub.Common;
    using CollectorHub.Data.Common.Repositories;
    using CollectorHub.Data.Models.Collections;
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
            if (!dbContext.CollectionTypes.Any())
            {
                this.SeedCollectionTypes(dbContext);
            }

            if (dbContext.Sortings.Any())
            {
                return;
            }
            else
            {
                await this.SeedSorting(dbContext);
            }
        }

        private void SeedCollectionTypes(ApplicationDbContext dbContext)
        {
            var legoType = new CollectionType();
            legoType.Name = GlobalConstants.LegoCollectionTypeName;

            var hotHWeelsType = new CollectionType();
            hotHWeelsType.Name = GlobalConstants.HotWheelsCollectionTypeName;

            dbContext.CollectionTypes.Add(legoType);
            dbContext.CollectionTypes.Add(hotHWeelsType);

            dbContext.SaveChanges();
        }

        private async Task SeedSorting(ApplicationDbContext dbContext)
        {
            var priceSortASC = new Sorting();
            priceSortASC.Name = GlobalConstants.SortingCheapestName;

            var priceSortDESC = new Sorting();
            priceSortDESC.Name = GlobalConstants.SortingExpensivestName;

            var dateCreatedASC = new Sorting();
            dateCreatedASC.Name = GlobalConstants.SortingOldestName;

            var dateCreatedDESC = new Sorting();
            dateCreatedDESC.Name = GlobalConstants.SortingNewestName;

            var viewCountASC = new Sorting();
            viewCountASC.Name = GlobalConstants.SortingLessViewedName;

            var viewCountDESC = new Sorting();
            viewCountDESC.Name = GlobalConstants.SortingMostViewedName;

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
