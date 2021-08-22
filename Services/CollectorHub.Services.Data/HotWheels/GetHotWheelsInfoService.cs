namespace CollectorHub.Services.Data.HotWheels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CollectorHub.Data.Common.Repositories;
    using CollectorHub.Data.Models.Common;
    using CollectorHub.Data.Models.Collections.HotWheels;
    using CollectorHub.Data.Models.User;
    using CollectorHub.Web.ViewModels.Collections;
    using CollectorHub.Web.ViewModels.Home;
    using CollectorHub.Web.ViewModels.Themes;

    public class GetHotWheelsInfoService : IGetHotWheelsInfoService
    {
        private readonly IDeletableEntityRepository<FastAndFuriousPremiumCar> ffpremiumCarsRepository;
        private readonly IDeletableEntityRepository<FastAndFuriousPremiumSerie> ffpremiumSeriesRepository;
        private readonly IDeletableEntityRepository<FastAndFuriousPremiumCollection> ffpremiumCollectionsRepository;
        private readonly IDeletableEntityRepository<FastAndFuriousPremiumItem> ffpremiumItemsRepository;
        private readonly IRepository<Category> categoriesRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> allUsers;

        public GetHotWheelsInfoService(
            IDeletableEntityRepository<FastAndFuriousPremiumCar> ffpremiumCarsRepository,
            IDeletableEntityRepository<FastAndFuriousPremiumSerie> ffpremiumSeriesRepository,
            IDeletableEntityRepository<FastAndFuriousPremiumCollection> ffpremiumCollectionsRepository,
            IDeletableEntityRepository<FastAndFuriousPremiumItem> ffpremiumItemsRepository,
            IRepository<Category> categoriesRepository,
            IDeletableEntityRepository<ApplicationUser> allUsers)
        {
            this.ffpremiumCarsRepository = ffpremiumCarsRepository;
            this.ffpremiumSeriesRepository = ffpremiumSeriesRepository;
            this.ffpremiumCollectionsRepository = ffpremiumCollectionsRepository;
            this.ffpremiumItemsRepository = ffpremiumItemsRepository;
            this.categoriesRepository = categoriesRepository;
            this.allUsers = allUsers;
        }

        public ApplicationUser GetUser(string userId)
        {
            return this.allUsers
                .All()
                .Where(x => x.Id == userId)
                .FirstOrDefault();
        }

        public ICollection<HotWheelsPremiumSeriesViewModel> GetAllPremiumSeriesAndCars()
        {
            var list = new List<HotWheelsPremiumSeriesViewModel>();

            foreach (var serie in this.ffpremiumSeriesRepository.All())
            {
                var currSerie = new HotWheelsPremiumSeriesViewModel();

                currSerie.Name = serie.Name;
                currSerie.Year = serie.Year;
                currSerie.Id = serie.Id;
                currSerie.OrderOfAppearence = serie.OrderOfApperance;

                list.Add(currSerie);
            }

            list = list.OrderBy(x => x.Year).ToList();

            var allCars = this.ffpremiumCarsRepository.All();

            foreach (var car in allCars)
            {
                foreach (var serie in list)
                {
                    if (car.Serie.Id == serie.Id)
                    {
                        serie.Cars.Add(car);
                    }

                    serie.Cars = serie.Cars.OrderBy(x => x.Col).ToList();
                }
            }

            list = list.OrderBy(x => x.OrderOfAppearence).ToList();

            return list;
        }

        public bool CheckIfUserCanCreateHWFFPremiumCollection(string userId)
        {
            var user = this.GetUser(userId);

            if (true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task CreateHotWheelsFastAndFuriousPremium(string userId, string description, bool isPublic, bool showPrices)
        {
            string defaultCollectionImageUrl = "https://previews.123rf.com/images/ratoca/ratoca1507/ratoca150700170/42144597-new-collection.jpg";

            var user = this.GetUser(userId);
            var categoryId = this.categoriesRepository
                .All()
                .Where(x => x.Name == "Hot Wheels")
                .Select(x => x.Id)
                .FirstOrDefault();

            var collection = new FastAndFuriousPremiumCollection();

            collection.UserId = userId;
            collection.User = user;
            collection.Description = description;
            collection.IsPublic = isPublic;
            collection.ShowPrices = showPrices;
            collection.Name = "Hot Wheels Fast and Furious Premium";
            collection.ImageUrl = defaultCollectionImageUrl;
            collection.ViewsCount = 0;
            collection.CategoryId = categoryId;

            Task.WaitAll(this.ffpremiumCollectionsRepository.AddAsync(collection));
            Task.WaitAll(this.allUsers.SaveChangesAsync());

            await this.ffpremiumCollectionsRepository.SaveChangesAsync();
        }

        public HotWheelsFastAndFuriousPremiumCollectionMyCollectionsViewModel GetHotWheelsFastAndFuriousPremiumCollection(string userId)
        {
            string defaultImageUrl = "http://hwcollectorsnews.com/wp-content/uploads/2019/09/Fat-original-Box-Set-1024x508.jpg";

            var model = new HotWheelsFastAndFuriousPremiumCollectionMyCollectionsViewModel();

            var collection = this.ffpremiumCollectionsRepository
                .All()
                .Where(x => x.UserId == userId)
                .FirstOrDefault();

            int totalCarsCount = this.ffpremiumCarsRepository
                .All()
                .Count();

            if (collection == null) //// if our model is null the view will know User do NOT have such collection and will not visiulize it
            {
                return null;
            }

            int ownedCarsCount = 0;
            if (collection.Items != null)
            {
                ownedCarsCount = collection.Items.GroupBy(x => x.CarId).Count(); // this way we select cars without repetitions /If user has 4 of exact same car he do not have 4 / 55 he has 1 / 55/
            }

            model.Id = collection.Id;
            model.Name = collection.Name;
            model.ViewsCount = collection.ViewsCount;
            model.Description = collection.Description;
            model.Progression = ownedCarsCount.ToString() + " / " + totalCarsCount.ToString() + " Cars owned";

            if (string.IsNullOrEmpty(collection.ImageUrl))
            {
                model.ImageUrl = defaultImageUrl;
            }
            else
            {
                model.ImageUrl = collection.ImageUrl;
            }

            return model;
        }

        public HotWheelsFastAndFuriousPremiumCollectionViewModel GetHotWheelsFastAndFuriousPremiumFullCollection(string collectionId)
        {
            string defaultImageUrl = "http://hwcollectorsnews.com/wp-content/uploads/2019/09/Fat-original-Box-Set-1024x508.jpg";

            var collection = this.ffpremiumCollectionsRepository
                .All()
                .Where(x => x.Id == collectionId)
                .FirstOrDefault();

            var model = new HotWheelsFastAndFuriousPremiumCollectionViewModel();

            // this should be removed from there ! this URL shoud be placed in its place when the entity is created man :0
            if (string.IsNullOrEmpty(collection.ImageUrl))
            {
                model.ImageUrl = defaultImageUrl;
            }
            else
            {
                model.ImageUrl = collection.ImageUrl;
            }

            model.Id = collection.Id;
            model.Name = collection.Name;
            model.IsPublic = collection.IsPublic;
            model.ShowPrices = collection.ShowPrices;
            model.User = collection.User;
            model.ViewsCount = collection.ViewsCount;
            model.Description = collection.Description;
            model.Items = this.ffpremiumItemsRepository.All().Where(x => x.CollectionId == collectionId).ToList();

            return model;
        }

        public async Task AddItemToFastAndFuriousPremiumCollection(string carId, string collectionId, decimal price, string customUrl)
        {
            var item = new FastAndFuriousPremiumItem();
            var collection = this.ffpremiumCollectionsRepository.All().Where(x => x.Id == collectionId).FirstOrDefault();

            item.Car = this.ffpremiumCarsRepository.All().Where(x => x.Id == carId).FirstOrDefault();
            item.Collection = collection;
            item.PriceBoughted = price;

            if (customUrl == null)
            {
                item.OwnerPictureUrl = item.Car.PhotoCardLink;
            }
            else
            {
                item.OwnerPictureUrl = customUrl;
            }

            collection.Items.Add(item);

            Task.WaitAll(this.ffpremiumItemsRepository.AddAsync(item));
            Task.WaitAll(this.ffpremiumCollectionsRepository.SaveChangesAsync());

            await this.ffpremiumItemsRepository.SaveChangesAsync();
        }

        public void RemoveItemFromFastAndFuriousPremiumCollection(string itemId, string collectionId)
        {
            var item = this.ffpremiumItemsRepository.All().Where(x => x.Id == itemId).FirstOrDefault();

            this.ffpremiumItemsRepository.Delete(item);

            this.ffpremiumItemsRepository.SaveChanges();
        }

        public void ChangePrivateOptionForCollection(string collectionId)
        {
            var collection = this.ffpremiumCollectionsRepository.All().Where(x => x.Id == collectionId).FirstOrDefault();

            if (collection.IsPublic)
            {
                collection.IsPublic = false;
            }
            else
            {
                collection.IsPublic = true;
            }

            this.ffpremiumCollectionsRepository.SaveChanges();
        }

        public void ChangeShowPricesOptionForCollection(string collectionId)
        {
            var collection = this.ffpremiumCollectionsRepository.All().Where(x => x.Id == collectionId).FirstOrDefault();

            if (collection.ShowPrices)
            {
                collection.ShowPrices = false;
            }
            else
            {
                collection.ShowPrices = true;
            }

            this.ffpremiumCollectionsRepository.SaveChanges();
        }

        public bool UserOwnsCollection(string userId, string collectionId)
        {
            var collection = this.ffpremiumCollectionsRepository
                .All()
                .Where(x => x.Id == collectionId)
                .FirstOrDefault();

            var currCollectionUserId = collection.UserId;

            if (currCollectionUserId == userId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CollectionExists(string collectionId)
        {
            var collection = this.ffpremiumCollectionsRepository
                .All()
                .Where(x => x.Id == collectionId)
                .FirstOrDefault();

            if (collection == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
