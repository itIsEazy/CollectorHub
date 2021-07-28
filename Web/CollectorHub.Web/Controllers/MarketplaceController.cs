namespace CollectorHub.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class MarketplaceController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
