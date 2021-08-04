namespace CollectorHub.Web.Controllers
{
    using System.Security.Claims;

    using CollectorHub.Services.Data.HotWheels;
    using CollectorHub.Web.ViewModels.Collections;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CollectionsController : BaseController
    {
        private readonly IGetHotWheelsInfoService hotWheelsInfoService;

        public CollectionsController(
            IGetHotWheelsInfoService hotWheelsInfoService)
        {
            this.hotWheelsInfoService = hotWheelsInfoService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult CreateHotWheelsFastAndFurious()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateHotWheelsFastAndFurious(CreateHotWheelsFastAndFuriousCollectionInputModel model)
        {
            return this.View();
        }

        [Authorize]
        public IActionResult CreateHotWheelsFastAndFuriousPremium()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!this.hotWheelsInfoService.CheckIfUserCanCreateHWFFPremiumCollection(userId))
            {
                return this.RedirectToAction(nameof(this.MyCollections));
            }

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateHotWheelsFastAndFuriousPremium(CreateHotWheelsFastAndFuriousPremiumCollectionInputModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!this.hotWheelsInfoService.CheckIfUserCanCreateHWFFPremiumCollection(userId))
            {
                return this.RedirectToAction(nameof(this.MyCollections));
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.hotWheelsInfoService.CreateHotWheelsFastAndFuriousPremium(userId, model.Description, model.IsPublic);

            return this.RedirectToAction(nameof(this.MyCollections));
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(CreateHotWheelsCollectionInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            // TODO: Redirect to each user collections page
            return this.Redirect("/");
        }

        public IActionResult MyCollections()
        {
            return this.View();
        }
    }
}
