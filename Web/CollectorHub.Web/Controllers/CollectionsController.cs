namespace CollectorHub.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;

    using CollectorHub.Services.Data.Category;
    using CollectorHub.Services.Data.Collections;
    using CollectorHub.Services.Data.HotWheels;
    using CollectorHub.Web.ViewModels.Collections;
    using CollectorHub.Web.ViewModels.Collections.Hot_Wheels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class CollectionsController : BaseController
    {
        private readonly IGetHotWheelsInfoService hotWheelsInfoService;
        private readonly ICollectionsService collectionsService;
        private readonly ICategoryService categoryService;

        public CollectionsController(
            IGetHotWheelsInfoService hotWheelsInfoService,
            ICollectionsService collectionsService,
            ICategoryService categoryService)
        {
            this.hotWheelsInfoService = hotWheelsInfoService;
            this.collectionsService = collectionsService;
            this.categoryService = categoryService;
        }

        [AllowAnonymous]
        public IActionResult Index(string categoryId)
        {
            if (!this.categoryService.CategoryExists(categoryId) && categoryId != null)
            {
                return this.BadRequest();
            }

            var model = this.collectionsService.GetIndexViewInformation(categoryId);
            return this.View(model);
        }

        public IActionResult Create()
        {
            var model = new CreateIndexViewModel();
            model.AllCollectionTypes = this.collectionsService.GetAllCollectionTypes();
            model.AllHotWheelsTypes = this.collectionsService.GetAllHotWheelsTypes();

            return this.View(model);
        }

        public IActionResult CreateHotWheelsCollection(string hotWheelsTypeId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!this.collectionsService.HotWheelsTypeExist(hotWheelsTypeId))
            {
                return this.BadRequest();
            }

            if (!this.collectionsService.CheckIfUserCanCreateHotWheelsCollection(userId, hotWheelsTypeId))
            {
                return this.RedirectToAction(nameof(this.Create));
            }

            var model = new CreateHotWheelsInputModel();
            model.HotWheelsTypeId = hotWheelsTypeId;
            model.HotWheelsTypeName = this.collectionsService.GetHotWheelsTypeName(hotWheelsTypeId);

            return this.View(model);
        }

        [HttpPost]
        public IActionResult CreateHotWheelsCollection(CreateHotWheelsInputModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!this.collectionsService.HotWheelsTypeExist(model.HotWheelsTypeId))
            {
                return this.BadRequest();
            }

            if (!this.collectionsService.CheckIfUserCanCreateHotWheelsCollection(userId, model.HotWheelsTypeId))
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            // TODO : this returns bool if it is false means something went wrong in the service and show message to user 'Collection not created please try again later'
            this.collectionsService.CreateHotWheelsCollection(userId, model.HotWheelsTypeId, model.Description, model.IsPublic, model.ShowPrices);

            return this.RedirectToAction(nameof(this.MyCollections));
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

            if (!this.hotWheelsInfoService.CollectionExists(collectionId))
            {
                return this.BadRequest();
            }

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
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            bool userOwnsCollection = false;

            if (this.hotWheelsInfoService.UserOwnsCollection(userId, model.SelectedModel.CollectionId))
            {
                userOwnsCollection = true;
            }

            if (!userOwnsCollection)
            {
                return this.BadRequest();
            }

            this.hotWheelsInfoService.AddItemToFastAndFuriousPremiumCollection(model.SelectedModel.CarId, model.SelectedModel.CollectionId, model.SelectedModel.PriceBoughted, model.SelectedModel.OwnerPictureUrl);

            return this.RedirectToAction(nameof(this.HotWheelsFastAndFuriousPremium), new { collectionId = model.SelectedModel.CollectionId });
            //// Redirects to : Collections/HotWheelsFastAndFuriousPremium?collectionId
        }

        public IActionResult RemoveHotWheelsFastAndFuriousPremiumItemFromCollection(HotWheelsFastAndFuriousPremiumCollectionViewModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            bool userOwnsCollection = false;

            if (this.hotWheelsInfoService.UserOwnsCollection(userId, model.SelectedModel.CollectionId))
            {
                userOwnsCollection = true;
            }

            if (!userOwnsCollection)
            {
                return this.BadRequest();
            }

            this.hotWheelsInfoService.RemoveItemFromFastAndFuriousPremiumCollection(model.SelectedModel.ItemId, model.SelectedModel.CollectionId);

            return this.RedirectToAction(nameof(this.HotWheelsFastAndFuriousPremium), new { collectionId = model.SelectedModel.CollectionId });
        }

        public IActionResult ChangePrivateOptionForCollection(HotWheelsFastAndFuriousPremiumCollectionViewModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!this.hotWheelsInfoService.CollectionExists(model.SelectedModel.CollectionId))
            {
                return this.BadRequest();
            }

            if (!this.hotWheelsInfoService.UserOwnsCollection(userId, model.SelectedModel.CollectionId))
            {
                return this.BadRequest();
            }

            this.hotWheelsInfoService.ChangePrivateOptionForCollection(model.SelectedModel.CollectionId);

            return this.RedirectToAction(nameof(this.HotWheelsFastAndFuriousPremium), new { collectionId = model.SelectedModel.CollectionId });
        }

        public IActionResult ChangeShowPricesOptionForCollection(HotWheelsFastAndFuriousPremiumCollectionViewModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!this.hotWheelsInfoService.CollectionExists(model.SelectedModel.CollectionId))
            {
                return this.BadRequest();
            }

            if (!this.hotWheelsInfoService.UserOwnsCollection(userId, model.SelectedModel.CollectionId))
            {
                return this.BadRequest();
            }

            this.hotWheelsInfoService.ChangeShowPricesOptionForCollection(model.SelectedModel.CollectionId);

            return this.RedirectToAction(nameof(this.HotWheelsFastAndFuriousPremium), new { collectionId = model.SelectedModel.CollectionId });
        }

        public IActionResult MyCollections()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var model = new MyCollectionIndexViewModel();
            model.HotWheelsCollections = this.collectionsService.GetMyCollectionHotWheelsCollections(userId);

            return this.View(model);
        }
    }
}
