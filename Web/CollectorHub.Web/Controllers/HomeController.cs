namespace CollectorHub.Web.Controllers
{
    using System.Diagnostics;

    using CollectorHub.Services.Data.HotWheels;
    using CollectorHub.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IGetHotWheelsInfoService hotWheelsInfoService;

        public HomeController(IGetHotWheelsInfoService hotWheelsInfoService)
        {
            this.hotWheelsInfoService = hotWheelsInfoService;
        }

        public IActionResult Index()
        {
            var hotWheelsModel = this.hotWheelsInfoService.GetInfo();
            return this.View(hotWheelsModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
