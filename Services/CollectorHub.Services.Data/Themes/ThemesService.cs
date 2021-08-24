namespace CollectorHub.Services.Data.Themes
{
    using System.Collections.Generic;
    using System.Linq;

    using CollectorHub.Data.Common.Repositories;
    using CollectorHub.Data.Models.Collections.HotWheels;
    using CollectorHub.Data.Models.Collections.Lego;
    using CollectorHub.Services.Data.Collections;
    using CollectorHub.Web.ViewModels.Themes;

    public class ThemesService : IThemesService
    {
        private readonly ICollectionsService collectionsService;
        private readonly IDeletableEntityRepository<HotWheelsCar> hotWheelsCarsRepository;
        private readonly IDeletableEntityRepository<HotWheelsSerie> hotWheelsSeriesRepository;
        private readonly IDeletableEntityRepository<LegoMinifigure> legoMinifiguresRepository;

        public ThemesService(
            ICollectionsService collectionsService,
            IDeletableEntityRepository<HotWheelsCar> hotWheelsCarsRepository,
            IDeletableEntityRepository<HotWheelsSerie> hotWheelsSeriesRepository,
            IDeletableEntityRepository<LegoMinifigure> legoMinifiguresRepository)
        {
            this.collectionsService = collectionsService;
            this.hotWheelsCarsRepository = hotWheelsCarsRepository;
            this.hotWheelsSeriesRepository = hotWheelsSeriesRepository;
            this.legoMinifiguresRepository = legoMinifiguresRepository;
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

        public IEnumerable<LegoThemeMinifigureModel> GetAllLegoFigures()
        {
            const string pictureLinkBegining = "https://img.bricklink.com/ItemImage/MN/0/sw";
            const string pictureLinkEnd = ".png";

            var list = new List<LegoThemeMinifigureModel>();

            var allMinifigs = this.legoMinifiguresRepository.All();

            foreach (var fig in allMinifigs)
            {
                var currFigure = new LegoThemeMinifigureModel
                {
                    Name = fig.Name,
                    SwNumber = fig.SwNumber,
                };

                currFigure.ImageUrl = pictureLinkBegining + fig.SwNumber + pictureLinkEnd;

                list.Add(currFigure);
            }

            return list;
        }
    }
}
