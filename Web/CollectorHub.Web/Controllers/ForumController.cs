namespace CollectorHub.Web.Controllers
{
    using CollectorHub.Data;
    using Microsoft.AspNetCore.Mvc;

    public class ForumController : Controller
    {
        public ForumController()
        {
        }

        public IActionResult Index()
        {
            return this.View();
        }
    }
}
