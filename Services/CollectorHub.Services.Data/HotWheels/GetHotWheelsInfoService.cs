namespace CollectorHub.Services.Data.HotWheels
{
    using System.Collections.Generic;
    using System.Linq;

    using CollectorHub.Data;
    using CollectorHub.Data.Common.Repositories;
    using CollectorHub.Data.Models.HotWheels;
    using CollectorHub.Web.ViewModels.Home;
    using CollectorHub.Web.ViewModels.Themes;

    public class GetHotWheelsInfoService : IGetHotWheelsInfoService
    {
        private readonly IRepository<PremiumHWCar> premiumHWCarsRepository;
        private readonly IRepository<PremiumHWSerie> premiumHWSeriesRepository;
        private readonly IRepository<PremiumHWCollection> premiumHWCollectionsRepository;

        public GetHotWheelsInfoService(
            IRepository<PremiumHWCar> premiumHWCarsRepository,
            IRepository<PremiumHWSerie> premiumHWSeriesRepository,
            IRepository<PremiumHWCollection> premiumHWCollectionsRepository)
        {
            this.premiumHWCarsRepository = premiumHWCarsRepository;
            this.premiumHWSeriesRepository = premiumHWSeriesRepository;
            this.premiumHWCollectionsRepository = premiumHWCollectionsRepository;
        }

        HotWheelsInfoViewModel IGetHotWheelsInfoService.GetInfo()
        {
            var data = new HotWheelsInfoViewModel
            {
                TotalHotWheelsCarsCount = this.premiumHWCarsRepository.All().Count(),
                TotalHotWheelsSeriesCount = this.premiumHWSeriesRepository.All().Count(),
                TotalHotWheelsCollectionsCount = this.premiumHWCollectionsRepository.All().Count(),
            };

            return data;
        }

        public ICollection<HotWheelsPremiumSeriesViewModel> GetAllPremiumSeriesAndCars()
        {
            var list = new List<HotWheelsPremiumSeriesViewModel>();

            foreach (var serie in this.premiumHWSeriesRepository.All())
            {
                var currSerie = new HotWheelsPremiumSeriesViewModel();

                currSerie.Name = serie.Name;
                currSerie.Year = serie.Year;
                currSerie.Id = serie.Id;

                list.Add(currSerie);
            }

            foreach (var car in this.premiumHWCarsRepository.All())
            {
                foreach (var serie in list)
                {
                    if (car.Serie.Id == serie.Id)
                    {
                        serie.Cars.Add(car);
                    }
                }
            }

            return list;
        }
    }
}
