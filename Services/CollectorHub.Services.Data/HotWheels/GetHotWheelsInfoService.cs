﻿namespace CollectorHub.Services.Data.HotWheels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CollectorHub.Data.Common.Repositories;
    using CollectorHub.Data.Models.HotWheels;
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
        private readonly IDeletableEntityRepository<ApplicationUser> allUsers;

        public GetHotWheelsInfoService(
            IDeletableEntityRepository<FastAndFuriousPremiumCar> ffpremiumCarsRepository,
            IDeletableEntityRepository<FastAndFuriousPremiumSerie> ffpremiumSeriesRepository,
            IDeletableEntityRepository<FastAndFuriousPremiumCollection> ffpremiumCollectionsRepository,
            IDeletableEntityRepository<FastAndFuriousPremiumItem> ffpremiumItemsRepository,
            IDeletableEntityRepository<ApplicationUser> allUsers)
        {
            this.ffpremiumCarsRepository = ffpremiumCarsRepository;
            this.ffpremiumSeriesRepository = ffpremiumSeriesRepository;
            this.ffpremiumCollectionsRepository = ffpremiumCollectionsRepository;
            this.ffpremiumItemsRepository = ffpremiumItemsRepository;
            this.allUsers = allUsers;
        }

        HotWheelsInfoViewModel IGetHotWheelsInfoService.GetInfo()
        {
            var data = new HotWheelsInfoViewModel
            {
                TotalHotWheelsCarsCount = this.ffpremiumCarsRepository.All().Count(),
                TotalHotWheelsSeriesCount = this.ffpremiumSeriesRepository.All().Count(),
                TotalHotWheelsCollectionsCount = this.ffpremiumCollectionsRepository.All().Count(),
            };

            return data;
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

            if (user.FFPremiumCollectionId == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task CreateHotWheelsFastAndFuriousPremium(string userId, string description, bool isPublic)
        {
            var user = this.GetUser(userId);

            var collection = new FastAndFuriousPremiumCollection();

            collection.UserId = userId;
            collection.User = user;
            collection.Description = description;
            collection.IsPublic = isPublic;
            collection.Name = "Hot Wheels Fast and Furious Premium";
            collection.ViewsCount = 0;

            user.FFPremiumCollectionId = collection.Id;
            user.FFPremiumCollection = collection;

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
            item.OwnerPictureUrl = customUrl;
            item.Collection = collection;
            item.PriceBoughted = price;

            collection.Items.Add(item);

            Task.WaitAll(this.ffpremiumItemsRepository.AddAsync(item));
            Task.WaitAll(this.ffpremiumCollectionsRepository.SaveChangesAsync());

            await this.ffpremiumItemsRepository.SaveChangesAsync();
        }
    }
}
