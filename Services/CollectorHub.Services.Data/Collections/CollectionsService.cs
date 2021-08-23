﻿namespace CollectorHub.Services.Data.Collections
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CollectorHub.Common;
    using CollectorHub.Data.Common.Repositories;
    using CollectorHub.Data.Models.Collections;
    using CollectorHub.Data.Models.Collections.HotWheels;
    using CollectorHub.Data.Models.Collections.Lego;
    using CollectorHub.Data.Models.User;
    using CollectorHub.Services.Data.Category;
    using CollectorHub.Services.Data.Common;
    using CollectorHub.Web.ViewModels.Collections;
    using CollectorHub.Web.ViewModels.Collections.Common;
    using CollectorHub.Web.ViewModels.Collections.Hot_Wheels;

    public class CollectionsService : ICollectionsService
    {
        private readonly ICategoryService categoryService;
        private readonly ICommonService commonService;
        private readonly IDeletableEntityRepository<FastAndFuriousPremiumCollection> ffpremiumCollectionsRepository;
        private readonly IDeletableEntityRepository<CollectionType> collectionTypesRepository;
        private readonly IDeletableEntityRepository<HotWheelsType> hotWheelsTypesRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<HotWheelsCollection> hotWheelsCollectionsRepository;
        private readonly IDeletableEntityRepository<HotWheelsCar> hotWheelsCarsRepository;
        private readonly IDeletableEntityRepository<HotWheelsCarItem> hotWheelsCarItemsRepository;
        private readonly IDeletableEntityRepository<HotWheelsSerie> hotWheelsSeriesRepository;
        private readonly IDeletableEntityRepository<LegoCollection> legoCollectionsRepository;

        public CollectionsService(
            ICategoryService categoryService,
            ICommonService commonService,
            IDeletableEntityRepository<FastAndFuriousPremiumCollection> ffpremiumCollectionsRepository,
            IDeletableEntityRepository<CollectionType> collectionTypesRepository,
            IDeletableEntityRepository<HotWheelsType> hotWheelsTypesRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<HotWheelsCollection> hotWheelsCollectionsRepository,
            IDeletableEntityRepository<HotWheelsCar> hotWheelsCarsRepository,
            IDeletableEntityRepository<HotWheelsCarItem> hotWheelsCarItemsRepository,
            IDeletableEntityRepository<HotWheelsSerie> hotWheelsSeriesRepository,
            IDeletableEntityRepository<LegoCollection> legoCollectionsRepository)
        {
            this.categoryService = categoryService;
            this.commonService = commonService;
            this.ffpremiumCollectionsRepository = ffpremiumCollectionsRepository;
            this.collectionTypesRepository = collectionTypesRepository;
            this.hotWheelsTypesRepository = hotWheelsTypesRepository;
            this.usersRepository = usersRepository;
            this.hotWheelsCollectionsRepository = hotWheelsCollectionsRepository;
            this.hotWheelsCarsRepository = hotWheelsCarsRepository;
            this.hotWheelsCarItemsRepository = hotWheelsCarItemsRepository;
            this.hotWheelsSeriesRepository = hotWheelsSeriesRepository;
            this.legoCollectionsRepository = legoCollectionsRepository;
        }

        public int GetAllCollectionsCount()
        {
            int totalCount = 0;

            int allHotWheelsCollectionsCount = this.GetAllHotWHeelsCollectionsCount();
            int allLegoCollectionsCount = this.GetAllLegoCollectionsCount();

            totalCount += allHotWheelsCollectionsCount;
            totalCount += allLegoCollectionsCount;

            return totalCount;
        }

        public ApplicationUser GetUser(string userId)
        {
            return this.usersRepository
                .All()
                .Where(x => x.Id == userId)
                .FirstOrDefault();
        }

        public List<TrendingCollectionViewModel> GetAllCollections()
        {
            const string hotWheelsCollectionAction = "HotWheelsCollection";
            const string legoCollectionAction = "LegoCollection";

            var allCollections = new List<TrendingCollectionViewModel>();

            //------------------------------- GET Collection----------------------------
            var hotWheelsCollections = this.hotWheelsCollectionsRepository
                .All()
                .Where(x => x.IsPublic)
                .OrderBy(x => x.ViewsCount)
                .Select(x => new
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Name = x.Name,
                    UserName = x.User.UserName,
                    Description = x.Description,
                    ViewsCount = x.ViewsCount,
                    IsPublic = x.IsPublic,
                    ShowPrices = x.ShowPrices,
                    ImageUrl = x.ImageUrl,
                    CategoryId = x.CategoryId,
                    DateCreated = x.CreatedOn.ToString("MM/dd/yyyy H:mm"),
                });

            var legoCollections = this.legoCollectionsRepository
               .All()
               .Where(x => x.IsPublic)
               .OrderBy(x => x.ViewsCount)
               .Select(x => new
               {
                   Id = x.Id,
                   UserId = x.UserId,
                   Name = x.Name,
                   UserName = x.User.UserName,
                   Description = x.Description,
                   ViewsCount = x.ViewsCount,
                   IsPublic = x.IsPublic,
                   ShowPrices = x.ShowPrices,
                   ImageUrl = x.ImageUrl,
                   CategoryId = x.CategoryId,
                   DateCreated = x.CreatedOn.ToString("MM/dd/yyyy H:mm"),
               });

            //------------------------------- ADD Collection----------------------------
            foreach (var collection in hotWheelsCollections)
            {
                allCollections.Add(new TrendingCollectionViewModel
                {
                    Id = collection.Id,
                    UserId = collection.UserId,
                    Name = collection.Name,
                    UserName = collection.UserName,
                    Description = collection.Description,
                    ViewsCount = collection.ViewsCount,
                    IsPublic = collection.IsPublic,
                    ShowPrices = collection.ShowPrices,
                    ImageUrl = collection.ImageUrl,
                    CategoryId = collection.CategoryId,
                    Action = hotWheelsCollectionAction,
                    DateCreated = collection.DateCreated,
                });
            }

            foreach (var collection in legoCollections)
            {
                allCollections.Add(new TrendingCollectionViewModel
                {
                    Id = collection.Id,
                    UserId = collection.UserId,
                    Name = collection.Name,
                    UserName = collection.UserName,
                    Description = collection.Description,
                    ViewsCount = collection.ViewsCount,
                    IsPublic = collection.IsPublic,
                    ShowPrices = collection.ShowPrices,
                    ImageUrl = collection.ImageUrl,
                    CategoryId = collection.CategoryId,
                    Action = legoCollectionAction,
                    DateCreated = collection.DateCreated,
                });
            }

            return allCollections;
        }

        public CollectionsIndexViewModel GetIndexViewInformation(string categoryId, string searchInput, int sortingId)
        {
            var model = new CollectionsIndexViewModel();

            model.Sortings = this.commonService.GetAllSortings();
            model.Categories = this.categoryService.GetAllCategories();

            var allCollections = this.GetAllCollections();

            //// if User is getting collection by CATEGORY Button
            if (categoryId != null)
            {
                allCollections = allCollections.Where(x => x.CategoryId == categoryId).ToList();

                model.CategoryName = this.categoryService
                    .GetAllCategories()
                    .Where(x => x.Id == categoryId)
                    .Select(x => x.Name)
                    .FirstOrDefault()
                    .ToString();
            }

            //// if User has Typed somethign in searchInput
            if (searchInput != null)
            {
                var searchedList = new List<TrendingCollectionViewModel>();
                List<string> words = searchInput.Split().ToList();

                foreach (var collection in allCollections)
                {
                    int wordMatchedCount = 0;

                    bool removeCurrentCollection = true;

                    foreach (var word in words)
                    {
                        if (collection.Name.ToLower().Contains(word))
                        {
                            removeCurrentCollection = false;
                            wordMatchedCount += 1;
                        }

                        if (collection.Description.ToLower().Contains(word))
                        {
                            removeCurrentCollection = false;
                            wordMatchedCount += 1;
                        }

                        if (wordMatchedCount >= 3)
                        {
                            break;
                        }
                    }

                    if (!removeCurrentCollection)
                    {
                        searchedList.Add(collection);
                    }
                }

                allCollections = searchedList;
            }

            //// if User is using sorting option
            if (sortingId != 0)
            {
                var sorting = this.commonService.GetAllSortings().Where(x => x.Id == sortingId).FirstOrDefault();

                if (sorting.Name == GlobalConstants.SortingNewestName)
                {
                    allCollections = allCollections.OrderByDescending(x => x.DateCreated).ToList();
                }
                else if (sorting.Name == GlobalConstants.SortingOldestName)
                {
                    allCollections = allCollections.OrderBy(x => x.DateCreated).ToList();
                }
                else if (sorting.Name == GlobalConstants.SortingMostViewedName)
                {
                    allCollections = allCollections.OrderByDescending(x => x.ViewsCount).ToList();
                }
                else if (sorting.Name == GlobalConstants.SortingLessViewedName)
                {
                    allCollections = allCollections.OrderBy(x => x.ViewsCount).ToList();
                }
            }

            model.TrendingCollectons = allCollections;

            return model;
        }

        public HotWheelsCollectionViewModel GetHotWheelsCollectionViewInformation(string collectionId)
        {
            var model = new HotWheelsCollectionViewModel();

            var collection = this.hotWheelsCollectionsRepository
                .All()
                .Where(x => x.Id == collectionId)
                .FirstOrDefault();

            var collectionHotWheelsTypeId = collection.HotWheelsTypeId;

            model.Id = collection.Id;
            model.UserName = collection.User.UserName;
            model.Name = collection.Name;
            model.ImageUrl = collection.ImageUrl;
            model.Description = collection.Description;
            model.IsPublic = collection.IsPublic;
            model.ShowPrices = collection.ShowPrices;

            var collectionSeries = this.hotWheelsSeriesRepository
                .All()
                .Where(x => x.HotWheelsTypeId == collectionHotWheelsTypeId)
                .OrderBy(x => x.OrderOfApperance)
                .Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    Year = x.Year,
                    OrderOfAppeareance = x.OrderOfApperance,
                });

            var allSeries = new List<HotWheelsCollectionSerieViewModel>();

            foreach (var serie in collectionSeries)
            {
                allSeries.Add(new HotWheelsCollectionSerieViewModel
                {
                    Id = serie.Id,
                    Name = serie.Name,
                    Year = serie.Year,
                    OrderOfAppearance = serie.OrderOfAppeareance,
                });
            }

            var allCars = this.hotWheelsCarsRepository
                .All()
                .Where(x => x.HotWheelsTypeId == collectionHotWheelsTypeId)
                .ToList();

            foreach (var serie in allSeries)
            {
                var cars = allCars.Where(x => x.SerieId == serie.Id);

                foreach (var car in cars)
                {
                    var carViewModel = new HotWheelsCollectionCarViewModel
                    {
                        Id = car.Id,
                        Col = car.Col,
                        Name = car.Name,
                        Movie = car.Movie,
                    };

                    if (car.PhotoCardLink != null)
                    {
                        carViewModel.ImageUrl = car.PhotoCardLink;
                    }
                    else
                    {
                        carViewModel.ImageUrl = car.PhotoLooseLink;
                    }

                    serie.Cars.Add(carViewModel);
                }

                serie.Cars = serie.Cars.OrderBy(x => x.Col).ToList();
            }

            model.AllSeries = allSeries;

            var collectionItems = this.hotWheelsCarItemsRepository
                .All()
                .Where(x => x.CollectionId == collectionId)
                .Select(x => new
                {
                    Id = x.Id,
                    PriceBoughted = x.PriceBoughted,
                    CarId = x.CarId,
                    CarSerieId = x.Car.SerieId,
                    Col = x.Car.Col,
                    Name = x.Car.Name,
                    Movie = x.Car.Movie,
                    ImageUrl = x.OwnerPictureUrl,
                });

            var allItems = new List<HotWheelsCollectionCarItemViewModel>();

            foreach (var item in collectionItems)
            {
                var currItem = new HotWheelsCollectionCarItemViewModel
                {
                    Id = item.Id,
                    PriceBoughted = item.PriceBoughted,
                    ImageUrl = item.ImageUrl,
                    SerieId = item.CarSerieId,
                };

                currItem.Car = new HotWheelsCollectionCarViewModel
                {
                    Id = item.CarId,
                    Col = item.Col,
                    Name = item.Name,
                    Movie = item.Movie,
                };

                allItems.Add(currItem);
            }

            var ownedSeries = new List<HotWheelsCollectionSerieViewModel>();

            foreach (var item in allItems)
            {
                var serie = allSeries
                    .Where(x => x.Id == item.SerieId)
                    .FirstOrDefault();

                if (!ownedSeries.Contains(serie))
                {
                    ownedSeries.Add(serie);
                }
            }

            model.OwnedSeries = ownedSeries.OrderBy(x => x.OrderOfAppearance).ToList();
            model.Items = allItems;

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

        public IEnumerable<HotWheelsCollectionViewModel> GetHotWheelsCollections(string userId)
        {
            string defaultImageUrl = "http://hwcollectorsnews.com/wp-content/uploads/2019/09/Fat-original-Box-Set-1024x508.jpg";

            var collections = this.hotWheelsCollectionsRepository
                .All()
                .Where(x => x.UserId == userId)
                .ToList();

            return null;
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

        public IEnumerable<MyCollectionHotWheelsCollectionViewModel> GetMyCollectionHotWheelsCollections(string userId)
        {
            var collections = this.hotWheelsCollectionsRepository
                .All()
                .Where(x => x.UserId == userId)
                .ToList();

            var list = new List<MyCollectionHotWheelsCollectionViewModel>();

            foreach (var collection in collections)
            {
                var currCollection = new MyCollectionHotWheelsCollectionViewModel();

                int totalCarsCount = this.hotWheelsCarsRepository
                    .All()
                    .Where(x => x.HotWheelsTypeId == collection.HotWheelsTypeId)
                    .Count();

                var ownedCarsCount = this.hotWheelsCarItemsRepository
                    .All()
                    .Where(x => x.CollectionId == collection.Id)
                    .GroupBy(x => x.CarId)
                    .Count();

                currCollection.Id = collection.Id;
                currCollection.Name = collection.Name;
                currCollection.ViewsCount = collection.ViewsCount;
                currCollection.Description = collection.Description;
                currCollection.Progression = ownedCarsCount.ToString() + " / " + totalCarsCount.ToString() + " Cars owned";
                currCollection.ImageUrl = collection.ImageUrl;

                list.Add(currCollection);
            }

            return list;
        }

        public IEnumerable<TrendingCollectionViewModel> GetTrendingCollections(string categoryId)
        {
            const string hotWheelsCollectionAction = "HotWheelsCollection";

            var allTrendingCollections = new List<TrendingCollectionViewModel>();

            var hotWheelsCollections = this.hotWheelsCollectionsRepository
                .All()
                .Where(x => x.IsPublic)
                .OrderBy(x => x.ViewsCount)
                .Select(x => new
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Name = x.Name,
                    UserName = x.User.UserName,
                    Description = x.Description,
                    ViewsCount = x.ViewsCount,
                    IsPublic = x.IsPublic,
                    ShowPrices = x.ShowPrices,
                    ImageUrl = x.ImageUrl,
                    CategoryId = x.CategoryId,
                });

            if (categoryId != null)
            {
                hotWheelsCollections = hotWheelsCollections.Where(x => x.CategoryId == categoryId);
            }

            foreach (var collection in hotWheelsCollections)
            {
                allTrendingCollections.Add(new TrendingCollectionViewModel
                {
                    Id = collection.Id,
                    UserId = collection.UserId,
                    Name = collection.Name,
                    UserName = collection.UserName,
                    Description = collection.Description,
                    ViewsCount = collection.ViewsCount,
                    IsPublic = collection.IsPublic,
                    ShowPrices = collection.ShowPrices,
                    ImageUrl = collection.ImageUrl,
                    CategoryId = collection.CategoryId,
                    Action = hotWheelsCollectionAction,
                });
            }

            return allTrendingCollections.Take(5);
        }

        public async Task CreateHotWheelsCollection(string userId, string hotWheelsTypeId, string description, bool isPublic, bool showPrices)
        {
            var collection = new HotWheelsCollection();

            collection.UserId = userId;
            collection.Description = description;
            collection.IsPublic = isPublic;
            collection.ShowPrices = showPrices;
            collection.Name = GlobalConstants.HotWheelsCategoryName + string.Empty + this.GetHotWheelsTypeName(hotWheelsTypeId);
            collection.ImageUrl = this.GetCollectionImageByHotWheelsType(hotWheelsTypeId);
            collection.ViewsCount = 0;
            collection.CategoryId = this.GetHotWheelsCategoryId();
            collection.CollectionTypeId = this.GetHotWheelsCollectionTypeId();
            collection.HotWheelsTypeId = hotWheelsTypeId;

            Task.WaitAll(this.hotWheelsCollectionsRepository.AddAsync(collection));

            this.hotWheelsCollectionsRepository.SaveChanges();
        }

        public async Task AddHotWheelsCarItemToCollection(string carId, string collectionId, decimal price, string customUrl)
        {
            var item = new HotWheelsCarItem();
            var collection = this.hotWheelsCollectionsRepository.All().Where(x => x.Id == collectionId).FirstOrDefault();

            item.CarId = carId;
            item.CollectionId = collectionId;
            item.PriceBoughted = price;

            string carImageUrl = this.hotWheelsCarsRepository
                .All()
                .Where(x => x.Id == carId)
                .Select(x => x.PhotoCardLink)
                .FirstOrDefault();

            if (customUrl == null)
            {
                item.OwnerPictureUrl = carImageUrl;
            }
            else
            {
                item.OwnerPictureUrl = customUrl;
            }

            Task.WaitAll(this.hotWheelsCarItemsRepository.AddAsync(item));

            this.hotWheelsCarItemsRepository.SaveChanges();
        }

        public void RemoveHotWheelsCarItemFromCollection(string itemId)
        {
            var item = this.hotWheelsCarItemsRepository.All().Where(x => x.Id == itemId).FirstOrDefault();

            this.hotWheelsCarItemsRepository.Delete(item);

            this.hotWheelsCarItemsRepository.SaveChanges();
        }

        public void ChangePrivateOptionForHotWheelsCollection(string collectionId)
        {
            var collection = this.hotWheelsCollectionsRepository.All().Where(x => x.Id == collectionId).FirstOrDefault();

            if (collection.IsPublic)
            {
                collection.IsPublic = false;
            }
            else
            {
                collection.IsPublic = true;
            }

            this.hotWheelsCollectionsRepository.SaveChanges();
        }

        public void ChangeShowPricesOptionForHotWheelsCollection(string collectionId)
        {
            var collection = this.hotWheelsCollectionsRepository.All().Where(x => x.Id == collectionId).FirstOrDefault();

            if (collection.ShowPrices)
            {
                collection.ShowPrices = false;
            }
            else
            {
                collection.ShowPrices = true;
            }

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

        public string GetHotWheelsCollectionUserId(string collectionId)
        {
            return this.hotWheelsCollectionsRepository
                .All()
                .Where(x => x.Id == collectionId)
                .Select(x => x.UserId)
                .FirstOrDefault();
        }

        //---------------------------THESE ALL MUST BE ABSTRACTED_ WHEN U PASS THE CLASS NAME IT SEARCHES IN ITS REPOSITORY-----------------------------------
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

        public bool HotWheelsCollectionExists(string collectionId)
        {
            var collection = this.hotWheelsCollectionsRepository.All().Where(x => x.Id == collectionId).FirstOrDefault();

            if (collection == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool HotWheelsCarExists(string carId)
        {
            var car = this.hotWheelsCarsRepository.All().Where(x => x.Id == carId).FirstOrDefault();

            if (car == null)
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
                .Where(x => x.UserId == userId && x.HotWheelsTypeId == hotWheelsTypeId)
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

        public bool CollectionIsPublic(string collectionId)
        {
            return this.hotWheelsCollectionsRepository
                .All()
                .Where(x => x.Id == collectionId)
                .Select(x => x.IsPublic)
                .FirstOrDefault();
        }

        public bool HotWheelsCarIsFromHotWHeelCollection(string collectionId, string carId)
        {
            var collectionHotWheelsTypeId = this.hotWheelsCollectionsRepository
                .All()
                .Where(x => x.Id == collectionId)
                .Select(x => x.HotWheelsTypeId)
                .FirstOrDefault();

            var carHotWheelsTypeId = this.hotWheelsCarsRepository
                .All()
                .Where(x => x.Id == carId)
                .Select(x => x.HotWheelsTypeId)
                .FirstOrDefault();

            if (collectionHotWheelsTypeId == carHotWheelsTypeId)
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
            return this.hotWheelsCollectionsRepository.All().Count();
        }

        private int GetAllLegoCollectionsCount()
        {
            return this.legoCollectionsRepository.All().Count();
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
