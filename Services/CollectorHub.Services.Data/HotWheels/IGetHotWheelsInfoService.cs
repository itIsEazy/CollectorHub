namespace CollectorHub.Services.Data.HotWheels
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CollectorHub.Data.Models.User;
    using CollectorHub.Web.ViewModels.Collections;
    using CollectorHub.Web.ViewModels.Home;
    using CollectorHub.Web.ViewModels.Themes;

    public interface IGetHotWheelsInfoService
    {
        HotWheelsInfoViewModel GetInfo();

        ApplicationUser GetUser(string userId);

        ICollection<HotWheelsPremiumSeriesViewModel> GetAllPremiumSeriesAndCars();

        bool CheckIfUserCanCreateHWFFPremiumCollection(string userId);

        Task CreateHotWheelsFastAndFuriousPremium(string userId, string description, bool isPublic);

        HotWheelsFastAndFuriousPremiumCollectionMyCollectionsViewModel GetHotWheelsFastAndFuriousPremiumCollection(string userId);

        HotWheelsFastAndFuriousPremiumCollectionViewModel GetHotWheelsFastAndFuriousPremiumFullCollection(string userId);

        Task AddItemToFastAndFuriousPremiumCollection(string carId, string collectionId, decimal price, string customUrl);
    }
}
