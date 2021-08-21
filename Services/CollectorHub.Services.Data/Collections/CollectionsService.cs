namespace CollectorHub.Services.Data.Collections
{
    using System.Collections.Generic;
    using System.Linq;

    using CollectorHub.Data.Common.Repositories;
    using CollectorHub.Data.Models.HotWheels;
    using CollectorHub.Services.Data.Category;
    using CollectorHub.Services.Data.Common;
    using CollectorHub.Web.ViewModels.Collections;

    public class CollectionsService : ICollectionsService
    {
        private readonly ICategoryService categoryService;
        private readonly ICommonService commonService;
        private readonly IDeletableEntityRepository<FastAndFuriousPremiumCollection> ffpremiumCollectionsRepository;

        public CollectionsService(
            ICategoryService categoryService,
            ICommonService commonService,
            IDeletableEntityRepository<FastAndFuriousPremiumCollection> ffpremiumCollectionsRepository)
        {
            this.categoryService = categoryService;
            this.commonService = commonService;
            this.ffpremiumCollectionsRepository = ffpremiumCollectionsRepository;
        }

        public int GetAllCollectionsCount()
        {
            int totalCount = 0;

            int allHotWheelsCollections = this.GetAllHotWHeelsCollectionsCount();
            totalCount += allHotWheelsCollections;

            return totalCount;
        }

        public CollectionsIndexViewModel GetIndexViewInformation(string categoryId)
        {
            var model = new CollectionsIndexViewModel();

            model.Sortings = this.commonService.GetAllSortings();
            model.Categories = this.categoryService.GetAllCategories();
            model.TrendingCollectons = this.GetAllTrendingCollections(categoryId);

            return model;
        }

        public IEnumerable<CollectionIndexViewModel> GetAllTrendingCollections(string categoryId)
        {
            // VERY IMPORTANT THIS MUST HAVE ALL THE STAR COUNT IN EVERY COLLECTION
            //// GETS ALL COLLECTIONS ORDERS THEM BY STAR ? VIEW COUNT AND RETURNS ONLY 5 10 15 20 25 !

            var list = new List<CollectionIndexViewModel>();

            var allHWFFPremiumCollections = this.GetAllFFPremiumCollections(categoryId);

            foreach (var collection in allHWFFPremiumCollections)
            {
                list.Add(collection);
            }

            list = list.OrderBy(x => x.ViewsCount).ToList();

            return list;
        }

        public List<CollectionIndexViewModel> GetAllFFPremiumCollections(string categoryId)
        {
            const string action = "HotWheelsFastAndFuriousPremium";

            var list = new List<CollectionIndexViewModel>();

            var allCollectionsAvailable = this.ffpremiumCollectionsRepository
                .All()
                .Where(x => x.IsPublic == true)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.ImageUrl,
                    x.User,
                    x.ViewsCount,
                    x.CategoryId,
                })
                .ToList();

            if (categoryId != null)
            {
                allCollectionsAvailable = allCollectionsAvailable.Where(x => x.CategoryId == categoryId).ToList();
            }

            foreach (var collection in allCollectionsAvailable)
            {
                list.Add(new CollectionIndexViewModel
                {
                    Id = collection.Id,
                    Name = collection.Name,
                    ImageUrl = collection.ImageUrl,
                    Owner = collection.User,
                    ViewsCount = collection.ViewsCount,
                    Action = action,
                });
            }

            return list;
        }

        private int GetAllHotWHeelsCollectionsCount()
        {
            int totalCount = 0;

            var allHWFFPremiumCollectionsCount = this.ffpremiumCollectionsRepository.All().Count();
            totalCount += allHWFFPremiumCollectionsCount;

            return totalCount;
        }
    }
}
