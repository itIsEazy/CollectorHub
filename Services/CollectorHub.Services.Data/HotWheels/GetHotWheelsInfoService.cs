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
        private readonly IRepository<FastAndFuriousPremiumCar> ffpremiumCarsRepository;
        private readonly IRepository<FastAndFuriousPremiumSerie> ffpremiumSeriesRepository;
        private readonly IRepository<FastAndFuriousPremiumCollection> ffpremiumCollectionsRepository;

        public GetHotWheelsInfoService(
            IRepository<FastAndFuriousPremiumCar> ffpremiumCarsRepository,
            IRepository<FastAndFuriousPremiumSerie> ffpremiumSeriesRepository,
            IRepository<FastAndFuriousPremiumCollection> ffpremiumCollectionsRepository)
        {
            this.ffpremiumCarsRepository = ffpremiumCarsRepository;
            this.ffpremiumSeriesRepository = ffpremiumSeriesRepository;
            this.ffpremiumCollectionsRepository = ffpremiumCollectionsRepository;
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
    }
}
