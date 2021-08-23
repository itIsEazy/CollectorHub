namespace CollectorHub.Services.Data.Themes
{
    using System.Linq;

    using CollectorHub.Data.Common.Repositories;
    using CollectorHub.Data.Models.Collections.HotWheels;
    using CollectorHub.Services.Data.Collections;
    using CollectorHub.Web.ViewModels.Themes;

    public class ThemesService : IThemesService
    {
        private readonly ICollectionsService collectionsService;
        private readonly IDeletableEntityRepository<HotWheelsCar> hotWheelsCarsRepository;
        private readonly IDeletableEntityRepository<HotWheelsSerie> hotWheelsSeriesRepository;

        public ThemesService(
            ICollectionsService collectionsService,
            IDeletableEntityRepository<HotWheelsCar> hotWheelsCarsRepository,
            IDeletableEntityRepository<HotWheelsSerie> hotWheelsSeriesRepository)
        {
            this.collectionsService = collectionsService;
            this.hotWheelsCarsRepository = hotWheelsCarsRepository;
            this.hotWheelsSeriesRepository = hotWheelsSeriesRepository;
        }

        public HotWheelsThemeViewModel GetAllHotWheelsInfo()
        {
            var model = new HotWheelsThemeViewModel();

            model.AllHotWheelsTypes = this.collectionsService.GetAllHotWheelsTypes();

            var allCars = this.hotWheelsCarsRepository
                .All()
                .Select(x => new
                {
                    Id = x.Id,
                    Col = x.Col,
                    PhotoCardLink = x.PhotoCardLink,
                    PhotoLooseLink = x.PhotoLooseLink,
                    Name = x.Name,
                    Movie = x.Movie,
                    HotWheelsTypeId = x.HotWheelsTypeId,
                    SerieId = x.SerieId,
                });

            foreach (var car in allCars)
            {
                var currCar = new HotWheelsThemeCarViewModel
                {
                    Id = car.Id,
                    Col = car.Col,
                    ImageUrl = car.PhotoCardLink,
                    Name = car.Name,
                    Movie = car.Movie,
                    HotWheelsTypeId = car.HotWheelsTypeId,
                    SerieId = car.SerieId,
                };

                if (currCar.ImageUrl == null)
                {
                    currCar.ImageUrl = car.PhotoLooseLink;
                }

                model.AllCars.Add(currCar);
            }

            var allSeries = this.hotWheelsSeriesRepository
                .All()
                .Select(x => new
                {
                    Id = x.Id,
                    Year = x.Year,
                    Name = x.Name,
                    HotWheelsTypeId = x.HotWheelsTypeId,
                    OrderOfAppearence = x.OrderOfApperance,
                });

            foreach (var serie in allSeries)
            {
                model.AllSeries.Add(new HotWheelsThemeSerieViewModel
                {
                    Id = serie.Id,
                    Year = serie.Year,
                    Name = serie.Name,
                    HotWheelsTypeId = serie.HotWheelsTypeId,
                    OrderOfAppearence = serie.OrderOfAppearence,
                });
            }

            return model;
        }
    }
}
