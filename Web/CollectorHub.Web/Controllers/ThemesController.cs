namespace CollectorHub.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ThemesController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
