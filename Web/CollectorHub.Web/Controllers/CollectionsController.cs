namespace CollectorHub.Web.Controllers
{
    using CollectorHub.Web.ViewModels.Collections;
    using Microsoft.AspNetCore.Mvc;

    public class CollectionsController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateHotWheelsCollectionInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            // TODO: Redirect to each user collections page
            return this.Redirect("/");
        }
    }
}
