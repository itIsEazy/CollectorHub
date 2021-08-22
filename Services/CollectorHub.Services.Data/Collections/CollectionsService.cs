namespace CollectorHub.Services.Data.Collections
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CollectorHub.Common;
    using CollectorHub.Data.Common.Repositories;
    using CollectorHub.Data.Models.Collections;
    using CollectorHub.Data.Models.Collections.HotWheels;
    using CollectorHub.Data.Models.User;
    using CollectorHub.Services.Data.Category;
    using CollectorHub.Services.Data.Common;
    using CollectorHub.Web.ViewModels.Collections;

    public class CollectionsService : ICollectionsService
    {
        private readonly ICategoryService categoryService;
        private readonly ICommonService commonService;
        private readonly IDeletableEntityRepository<FastAndFuriousPremiumCollection> ffpremiumCollectionsRepository;
        private readonly IDeletableEntityRepository<CollectionType> collectionTypesRepository;
        private readonly IDeletableEntityRepository<HotWheelsType> hotWheelsTypesRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<HotWheelsCollection> hotWheelsCollectionsRepository;

        public CollectionsService(
            ICategoryService categoryService,
            ICommonService commonService,
            IDeletableEntityRepository<FastAndFuriousPremiumCollection> ffpremiumCollectionsRepository,
            IDeletableEntityRepository<CollectionType> collectionTypesRepository,
            IDeletableEntityRepository<HotWheelsType> hotWheelsTypesRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<HotWheelsCollection> hotWheelsCollectionsRepository)
        {
            this.categoryService = categoryService;
            this.commonService = commonService;
            this.ffpremiumCollectionsRepository = ffpremiumCollectionsRepository;
            this.collectionTypesRepository = collectionTypesRepository;
            this.hotWheelsTypesRepository = hotWheelsTypesRepository;
            this.usersRepository = usersRepository;
            this.hotWheelsCollectionsRepository = hotWheelsCollectionsRepository;
        }

        public int GetAllCollectionsCount()
        {
            int totalCount = 0;

            int allHotWheelsCollections = this.GetAllHotWHeelsCollectionsCount();
            totalCount += allHotWheelsCollections;

            return totalCount;
        }

        public ApplicationUser GetUser(string userId)
        {
            return this.usersRepository
                .All()
                .Where(x => x.Id == userId)
                .FirstOrDefault();
        }

        public CollectionsIndexViewModel GetIndexViewInformation(string categoryId)
        {
            var model = new CollectionsIndexViewModel();

            model.Sortings = this.commonService.GetAllSortings();
            model.Categories = this.categoryService.GetAllCategories();
            model.TrendingCollectons = this.GetAllTrendingCollections(categoryId);

            return model;
        }

        public IEnumerable<CollectionType> GetAllCollectionTypes()
        {
            return this.collectionTypesRepository.All().ToList();
        }

        public IEnumerable<HotWheelsType> GetAllHotWheelsTypes()
        {
            return this.hotWheelsTypesRepository.All().ToList();
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

        public async Task CreateHotWheelsCollection(string userId, string hotWheelsTypeId, string description, bool isPublic, bool showPrices)
        {
            var collection = new HotWheelsCollection();

            collection.UserId = userId;
            collection.Description = description;
            collection.IsPublic = isPublic;
            collection.ShowPrices = showPrices;
            collection.Name = GlobalConstants.HotWheelsCategoryName + this.GetHotWheelsTypeName(hotWheelsTypeId);
            collection.ImageUrl = this.GetCollectionImageByHotWheelsType(hotWheelsTypeId);
            collection.ViewsCount = 0;
            collection.CategoryId = this.GetHotWheelsCategoryId();
            collection.CollectionTypeId = this.GetHotWheelsCollectionTypeId();
            collection.TypeId = hotWheelsTypeId;

            Task.WaitAll(this.hotWheelsCollectionsRepository.AddAsync(collection));

            this.hotWheelsCollectionsRepository.SaveChanges();
        }

        public string GetHotWheelsTypeName(string hotWheelsTypeId)
        {
            if (this.HotWheelsTypeExist(hotWheelsTypeId))
            {
                return this.hotWheelsTypesRepository
                    .All()
                    .Where(x => x.Id == hotWheelsTypeId)
                    .Select(x => x.Name)
                    .FirstOrDefault();
            }

            return null;
        }

        public string GetHotWheelsTypeImageUrl(string hotWheelsTypeId)
        {
            if (this.HotWheelsTypeExist(hotWheelsTypeId))
            {
                return this.hotWheelsTypesRepository
                    .All()
                    .Where(x => x.Id == hotWheelsTypeId)
                    .Select(x => x.ImageUrl)
                    .FirstOrDefault();
            }

            return null;
        }

        public bool HotWheelsTypeExist(string typeId)
        {
            var type = this.hotWheelsTypesRepository.All().Where(x => x.Id == typeId).FirstOrDefault();

            if (type == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CheckIfUserCanCreateHotWheelsCollection(string userId, string hotWheelsTypeId)
        {
            var collection = this.hotWheelsCollectionsRepository
                .All()
                .Where(x => x.UserId == userId && x.TypeId == hotWheelsTypeId)
                .FirstOrDefault();

            if (collection == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int GetAllHotWHeelsCollectionsCount()
        {
            int totalCount = 0;

            var allHWFFPremiumCollectionsCount = this.ffpremiumCollectionsRepository.All().Count();
            totalCount += allHWFFPremiumCollectionsCount;

            return totalCount;
        }

        private string GetHotWheelsCategoryId()
        {
            return this.categoryService
                .GetAllCategories()
                .Where(x => x.Name == GlobalConstants.HotWheelsCategoryName)
                .Select(x => x.Id)
                .FirstOrDefault();
        }

        private string GetHotWheelsCollectionTypeId()
        {
            return this.collectionTypesRepository
                .All()
                .Where(x => x.Name == GlobalConstants.HotWheelsCollectionTypeName)
                .Select(x => x.Id)
                .FirstOrDefault();
        }

        private string GetCollectionImageByHotWheelsType(string hotWheelsTypeId)
        {
            string defaultCollectionImageUrl = "https://previews.123rf.com/images/ratoca/ratoca1507/ratoca150700170/42144597-new-collection.jpg";
            string imageUrl = this.GetHotWheelsTypeImageUrl(hotWheelsTypeId);
            if (imageUrl == null)
            {
                imageUrl = defaultCollectionImageUrl;
            }

            return imageUrl;
        }
    }
}
