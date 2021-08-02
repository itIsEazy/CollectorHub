namespace CollectorHub.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class StatisticsController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
