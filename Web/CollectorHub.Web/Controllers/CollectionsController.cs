namespace CollectorHub.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;

    using CollectorHub.Services.Data.HotWheels;
    using CollectorHub.Web.ViewModels.Collections;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class CollectionsController : BaseController
    {
        private readonly IGetHotWheelsInfoService hotWheelsInfoService;

        public CollectionsController(
            IGetHotWheelsInfoService hotWheelsInfoService)
        {
            this.hotWheelsInfoService = hotWheelsInfoService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Create()
        {
            return this.View();
        }

        public IActionResult CreateHotWheelsFastAndFurious()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult CreateHotWheelsFastAndFurious(CreateHotWheelsFastAndFuriousCollectionInputModel model)
        {
            return this.View();
        }

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

            this.hotWheelsInfoService.CreateHotWheelsFastAndFuriousPremium(userId, model.Description, model.IsPublic, model.ShowPrices);

            return this.RedirectToAction(nameof(this.MyCollections));
        }

        public IActionResult HotWheelsFastAndFuriousPremium(string collectionId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            this.ViewBag.BrowsingUserId = userId;

            var model = this.hotWheelsInfoService.GetHotWheelsFastAndFuriousPremiumFullCollection(collectionId);

            if (userId != model.User.Id && model.IsPublic == false)
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            model.AllSeries = this.hotWheelsInfoService.GetAllPremiumSeriesAndCars().ToList();

            return this.View(model);
        }

        public IActionResult AddHotWheelsFastAndFuriousPremiumItemToCollection(HotWheelsFastAndFuriousPremiumCollectionViewModel model)
        {
            this.hotWheelsInfoService.AddItemToFastAndFuriousPremiumCollection(model.SelectedModel.CarId, model.SelectedModel.CollectionId, model.SelectedModel.PriceBoughted, model.SelectedModel.OwnerPictureUrl);

            return this.RedirectToAction(nameof(this.HotWheelsFastAndFuriousPremium), new { collectionId = model.SelectedModel.CollectionId });
            //// Redirects to : Collections/HotWheelsFastAndFuriousPremium?collectionId
        }

        public IActionResult RemoveHotWheelsFastAndFuriousPremiumItemFromCollection(HotWheelsFastAndFuriousPremiumCollectionViewModel model)
        {
            this.hotWheelsInfoService.RemoveItemFromFastAndFuriousPremiumCollection(model.SelectedModel.ItemId, model.SelectedModel.CollectionId);

            return this.RedirectToAction(nameof(this.HotWheelsFastAndFuriousPremium), new { collectionId = model.SelectedModel.CollectionId });
        }

        public IActionResult MyCollections(HotWheelsFastAndFuriousPremiumCollectionMyCollectionsViewModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            model = this.hotWheelsInfoService.GetHotWheelsFastAndFuriousPremiumCollection(userId);
            return this.View(model);
        }
    }
}
