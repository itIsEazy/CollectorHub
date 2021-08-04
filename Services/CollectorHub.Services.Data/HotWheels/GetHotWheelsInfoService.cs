namespace CollectorHub.Services.Data.HotWheels
{
    using System.Collections.Generic;
    using System.Linq;

    using CollectorHub.Data.Common.Repositories;
    using CollectorHub.Data.Models.HotWheels;
    using CollectorHub.Data.Models.User;
    using CollectorHub.Web.ViewModels.Home;
    using CollectorHub.Web.ViewModels.Themes;

    public class GetHotWheelsInfoService : IGetHotWheelsInfoService
    {
        private readonly IDeletableEntityRepository<FastAndFuriousPremiumCar> ffpremiumCarsRepository;
        private readonly IDeletableEntityRepository<FastAndFuriousPremiumSerie> ffpremiumSeriesRepository;
        private readonly IDeletableEntityRepository<FastAndFuriousPremiumCollection> ffpremiumCollectionsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> allUsers;

        public GetHotWheelsInfoService(
            IDeletableEntityRepository<FastAndFuriousPremiumCar> ffpremiumCarsRepository,
            IDeletableEntityRepository<FastAndFuriousPremiumSerie> ffpremiumSeriesRepository,
            IDeletableEntityRepository<FastAndFuriousPremiumCollection> ffpremiumCollectionsRepository,
            IDeletableEntityRepository<ApplicationUser> allUsers)
        {
            this.ffpremiumCarsRepository = ffpremiumCarsRepository;
            this.ffpremiumSeriesRepository = ffpremiumSeriesRepository;
            this.ffpremiumCollectionsRepository = ffpremiumCollectionsRepository;
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

            foreach (var car in this.ffpremiumCarsRepository.All())
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

        public void CreateHotWheelsFastAndFuriousPremium(string userId, string description, bool isPublic)
        {
            var user = this.GetUser(userId);

            var collection = new FastAndFuriousPremiumCollection();

            collection.UserId = userId;
            collection.Description = description;
            collection.IsPublic = isPublic;
            collection.Name = "Hot Wheels Fast and Furious Premium";
            collection.ViewsCount = 0;

            user.FFPremiumCollectionId = collection.Id;

            this.allUsers.SaveChangesAsync();
            this.ffpremiumCollectionsRepository.AddAsync(collection);
            this.ffpremiumCollectionsRepository.SaveChangesAsync();
        }
    }
}
