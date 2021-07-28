namespace CollectorHub.Services.Data.HotWheels
{
    using System.Linq;

    using CollectorHub.Data.Common.Repositories;
    using CollectorHub.Data.Models.HotWheels;
    using CollectorHub.Web.ViewModels.Home;

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
    }
}
