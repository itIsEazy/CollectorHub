namespace CollectorHub.Web.Controllers
{
    using CollectorHub.Data;
    using Microsoft.AspNetCore.Mvc;

    public class NewsController : Controller
    {
        public NewsController()
        {
        }

        public IActionResult Index()
        {
            return this.View();
        }
    }
}
