namespace CollectorHub.Web.Controllers
{
    using System.Diagnostics;

    using CollectorHub.Services.Data.Administration;
    using CollectorHub.Services.Data.HotWheels;
    using CollectorHub.Web.ViewModels;
    using CollectorHub.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IGetHotWheelsInfoService hotWheelsInfoService;
        private readonly IAdministrationService administrationService;

        public HomeController(
            IGetHotWheelsInfoService hotWheelsInfoService,
            IAdministrationService administrationService)
        {
            this.hotWheelsInfoService = hotWheelsInfoService;
            this.administrationService = administrationService;
        }

        public IActionResult Index()
        {
            var hotWheelsModel = this.hotWheelsInfoService.GetInfo();
            return this.View(hotWheelsModel);
        }

        [Authorize]
        public IActionResult BecomeAdmin()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult BecomeAdmin(BecomeAdminViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (this.administrationService.AddNewAdmin(this.User.Identity.Name, input.UniquePassword).Result)
            {
                return this.RedirectToAction(nameof(this.Index));
            }
            else
            {
                return this.View(input);
            }
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
