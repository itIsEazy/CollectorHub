namespace CollectorHub.Services.Data.HotWheels
{
    using System.Collections.Generic;

    using CollectorHub.Web.ViewModels.Home;
    using CollectorHub.Web.ViewModels.Themes;

    public interface IGetHotWheelsInfoService
    {
        HotWheelsInfoViewModel GetInfo();

        ICollection<HotWheelsPremiumSeriesViewModel> GetAllPremiumSeriesAndCars();
    }
}
