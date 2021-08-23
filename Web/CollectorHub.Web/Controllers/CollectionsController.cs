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
        public IActionResult Index(string categoryId, CollectionsIndexViewModel model)
        {
            var currCategoryId = categoryId;
            string searchInput = null;
            int sortingid = 0;

            if (categoryId == null && model.SearchModel != null)
            {
                currCategoryId = model.SearchModel.CategoryId;
                searchInput = model.SearchModel.SearchInput;
                sortingid = model.SearchModel.SortingId;
            }

            if (currCategoryId != null && !this.categoryService.CategoryExists(currCategoryId))
            {
                return this.BadRequest();
            }

            model = this.collectionsService.GetIndexViewInformation(currCategoryId, searchInput, sortingid);

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

        public IActionResult AddHotWheelsCarItemToCollection(HotWheelsCollectionViewModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!this.collectionsService.HotWheelsCollectionExists(model.SelectedModel.CollectionId))
            {
                return this.BadRequest();
            }

            if (!this.collectionsService.HotWheelsCarExists(model.SelectedModel.CarId))
            {
                return this.BadRequest();
            }

            var collectionUserId = this.collectionsService.GetHotWheelsCollectionUserId(model.SelectedModel.CollectionId);

            if (userId != collectionUserId)
            {
                return this.BadRequest();
            }

            if (!this.collectionsService.HotWheelsCarIsFromHotWHeelCollection(model.SelectedModel.CollectionId, model.SelectedModel.CarId))
            {
                return this.BadRequest();
            }

            this.collectionsService.AddHotWheelsCarItemToCollection(model.SelectedModel.CarId, model.SelectedModel.CollectionId, model.SelectedModel.PriceBoughted, model.SelectedModel.OwnerImageUrl);

            return this.RedirectToAction(nameof(this.HotWheelsCollection), new { collectionId = model.SelectedModel.CollectionId });
            //// Redirects to : Collections/HotWheelsFastAndFuriousPremium?collectionId
        }

        public IActionResult RemoveHotWheelsCarItemFromCollection(HotWheelsCollectionViewModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!this.collectionsService.HotWheelsCollectionExists(model.SelectedModel.CollectionId))
            {
                return this.BadRequest();
            }

            if (!this.collectionsService.HotWheelsCarExists(model.SelectedModel.CarId))
            {
                return this.BadRequest();
            }

            var collectionUserId = this.collectionsService.GetHotWheelsCollectionUserId(model.SelectedModel.CollectionId);

            if (userId != collectionUserId)
            {
                return this.BadRequest();
            }

            if (!this.collectionsService.HotWheelsCarIsFromHotWHeelCollection(model.SelectedModel.CollectionId, model.SelectedModel.CarId))
            {
                return this.BadRequest();
            }

            this.collectionsService.RemoveHotWheelsCarItemFromCollection(model.SelectedModel.ItemId);

            return this.RedirectToAction(nameof(this.HotWheelsCollection), new { collectionId = model.SelectedModel.CollectionId });
            //// Redirects to : Collections/HotWheelsFastAndFuriousPremium?collectionId
        }

        public IActionResult ChangePrivateOptionForHotWheelsCollection(HotWheelsCollectionViewModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!this.collectionsService.HotWheelsCollectionExists(model.SelectedModel.CollectionId))
            {
                return this.BadRequest();
            }

            var collectionUserId = this.collectionsService.GetHotWheelsCollectionUserId(model.SelectedModel.CollectionId);

            if (userId != collectionUserId)
            {
                return this.BadRequest();
            }

            this.collectionsService.ChangePrivateOptionForHotWheelsCollection(model.SelectedModel.CollectionId);

            return this.RedirectToAction(nameof(this.HotWheelsCollection), new { collectionId = model.SelectedModel.CollectionId });
        }

        public IActionResult ChangeShowPricesOptionForHotWheelsCollection(HotWheelsFastAndFuriousPremiumCollectionViewModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!this.collectionsService.HotWheelsCollectionExists(model.SelectedModel.CollectionId))
            {
                return this.BadRequest();
            }

            var collectionUserId = this.collectionsService.GetHotWheelsCollectionUserId(model.SelectedModel.CollectionId);

            if (userId != collectionUserId)
            {
                return this.BadRequest();
            }

            this.collectionsService.ChangeShowPricesOptionForHotWheelsCollection(model.SelectedModel.CollectionId);

            return this.RedirectToAction(nameof(this.HotWheelsCollection), new { collectionId = model.SelectedModel.CollectionId });
        }

        public IActionResult MyCollections()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var model = new MyCollectionIndexViewModel();
            model.HotWheelsCollections = this.collectionsService.GetMyCollectionHotWheelsCollections(userId);

            return this.View(model);
        }

        public IActionResult HotWheelsCollection(string collectionId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var collectionUserId = this.collectionsService.GetHotWheelsCollectionUserId(collectionId);

            if (!this.collectionsService.HotWheelsCollectionExists(collectionId))
            {
                return this.BadRequest();
            }

            // Collection is private AND user IS NOT owner
            if (!this.collectionsService.CollectionIsPublic(collectionId) && userId != collectionUserId)
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewBag.UserIsOwner = false;
            if (userId == collectionUserId)
            {
                this.ViewBag.UserIsOwner = true;
            }

            var model = this.collectionsService.GetHotWheelsCollectionViewInformation(collectionId);

            return this.View(model);
        }
    }
}
