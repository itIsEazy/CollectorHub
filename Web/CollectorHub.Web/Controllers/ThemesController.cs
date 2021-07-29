namespace CollectorHub.Web.Controllers
{
    using CollectorHub.Services.Data.HotWheels;
    using CollectorHub.Web.ViewModels.Themes;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    public class ThemesController : Controller
    {
        private readonly IGetHotWheelsInfoService hotWheelsInfoService;

        public ThemesController(IGetHotWheelsInfoService hotWheelsInfoService)
        {
            this.hotWheelsInfoService = hotWheelsInfoService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult HotWheelsAll()
        {
            List<HotWheelsPremiumSeriesViewModel> model = this.hotWheelsInfoService.GetAllPremiumSeriesAndCars().ToList();

            return this.View(model);
        }

        public IActionResult LegoAll()
        {
            return this.View();
        }
    }
}
