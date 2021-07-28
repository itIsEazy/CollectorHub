namespace CollectorHub.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ForumController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
