namespace CollectorHub.Services.Data.HotWheels
{
    using CollectorHub.Web.ViewModels.Home;

    public interface IGetHotWheelsInfoService
    {
        HotWheelsInfoViewModel GetInfo();
    }
}
