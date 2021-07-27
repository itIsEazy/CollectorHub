namespace CollectorHub.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using CollectorHub.Data;
    using CollectorHub.Web.ViewModels;
    using CollectorHub.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ApplicationDbContext db;

        public HomeController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            

            var hotWheelsModel = new HotWheelsInfoViewModel
            {
                TotalHotWheelsCarsCount = this.db.PremiumHWCars.Count(),
                TotalHotWheelsSeriesCount = this.db.PremiumHWSeries.Count(),
            };

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
