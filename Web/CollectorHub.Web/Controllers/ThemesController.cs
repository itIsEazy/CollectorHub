namespace CollectorHub.Web.Controllers
{
    using CollectorHub.Services.Data.Themes;
    using CollectorHub.Web.ViewModels.Themes;
    using Microsoft.AspNetCore.Mvc;

    public class ThemesController : Controller
    {
        private readonly IThemesService themesService;

        public ThemesController(IThemesService themesService)
        {
            this.themesService = themesService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult HotWheelsAll()
        {
            var model = this.themesService.GetAllHotWheelsInfo();

            return this.View(model);
        }

        public IActionResult LegoAll()
        {
            var model = new LegoThemeViewModel();
            model.LegoFigures = this.themesService.GetAllLegoFigures();

            return this.View(model);
        }
    }
}
