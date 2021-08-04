namespace CollectorHub.Services.Data.HotWheels
{
    using System.Collections.Generic;

    using CollectorHub.Data.Models.User;
    using CollectorHub.Web.ViewModels.Home;
    using CollectorHub.Web.ViewModels.Themes;

    public interface IGetHotWheelsInfoService
    {
        HotWheelsInfoViewModel GetInfo();

        ApplicationUser GetUser(string userId);

        ICollection<HotWheelsPremiumSeriesViewModel> GetAllPremiumSeriesAndCars();

        bool CheckIfUserCanCreateHWFFPremiumCollection(string userId);

        void CreateHotWheelsFastAndFuriousPremium(string userId, string description, bool isPublic);
    }
}
