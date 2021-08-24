namespace CollectorHub.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;

    using CollectorHub.Services.Data.Category;
    using CollectorHub.Services.Data.Collections;
    using CollectorHub.Web.ViewModels.Collections;
    using CollectorHub.Web.ViewModels.Collections.Hot_Wheels;
    using CollectorHub.Web.ViewModels.Collections.Lego;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class CollectionsController : BaseController
    {
        private readonly ICollectionsService collectionsService;
        private readonly ICategoryService categoryService;

        public CollectionsController(
            ICollectionsService collectionsService,
            ICategoryService categoryService)
        {
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
            model.AllLegoTypes = this.collectionsService.GetAllLegoTypes();

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

        public IActionResult CreateLegoCollection(string legoTypeId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!this.collectionsService.LegoTypeExists(legoTypeId))
            {
                return this.BadRequest();
            }

            var model = new CreateLegoInputModel();
            model.LegoTypeId = legoTypeId;
            model.LegoTypeName = this.collectionsService.GetLegoTypeName(legoTypeId);

            return this.View(model);
        }

        [HttpPost]
        public IActionResult CreateLegoCollection(CreateLegoInputModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!this.collectionsService.LegoTypeExists(model.LegoTypeId))
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            // TODO : this should return bool if it is false means something went wrong in the service and show message to user 'Collection not created please try again later'
            this.collectionsService.CreateLegoCollection(userId, model.LegoTypeId, model.Description, model.IsPublic, model.ShowPrices);

            return this.RedirectToAction(nameof(this.MyCollections));
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

        public IActionResult AddLegoMinifigureItemToCollection(LegoCollectionViewModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!this.collectionsService.LegoCollectionExists(model.SelectedModel.CollectionId))
            {
                return this.BadRequest();
            }

            if (!this.collectionsService.LegoMinifigureExists(model.SelectedModel.MinifigureId))
            {
                return this.BadRequest();
            }

            var collectionUserId = this.collectionsService.GetLegoCollectionUserId(model.SelectedModel.CollectionId);

            if (userId != collectionUserId)
            {
                return this.BadRequest();
            }

            this.collectionsService.AddLegoMinifigureItemToCollection(model.SelectedModel.MinifigureId, model.SelectedModel.CollectionId, model.SelectedModel.PriceBoughted, model.SelectedModel.OwnerImageUrl);

            return this.RedirectToAction(nameof(this.LegoCollection), new { collectionId = model.SelectedModel.CollectionId });
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

        public IActionResult RemoveLegoMinifigureItemFromCollection(LegoCollectionViewModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!this.collectionsService.LegoCollectionExists(model.SelectedModel.CollectionId))
            {
                return this.BadRequest();
            }

            if (!this.collectionsService.LegoMinifigureExists(model.SelectedModel.MinifigureId))
            {
                return this.BadRequest();
            }

            var collectionUserId = this.collectionsService.GetLegoCollectionUserId(model.SelectedModel.CollectionId);

            if (userId != collectionUserId)
            {
                return this.BadRequest();
            }

            this.collectionsService.RemoveLegoMinifigureItemFromCollection(model.SelectedModel.ItemId);

            return this.RedirectToAction(nameof(this.LegoCollection), new { collectionId = model.SelectedModel.CollectionId });
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

        public IActionResult ChangePrivateOptionForLegoCollection(LegoCollectionViewModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!this.collectionsService.LegoCollectionExists(model.SelectedModel.CollectionId))
            {
                return this.BadRequest();
            }

            var collectionUserId = this.collectionsService.GetLegoCollectionUserId(model.SelectedModel.CollectionId);

            if (userId != collectionUserId)
            {
                return this.BadRequest();
            }

            this.collectionsService.ChangePrivateOptionForLegoCollection(model.SelectedModel.CollectionId);

            return this.RedirectToAction(nameof(this.LegoCollection), new { collectionId = model.SelectedModel.CollectionId });
        }

        public IActionResult ChangeShowPricesOptionForHotWheelsCollection(HotWheelsCollectionViewModel model)
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

        public IActionResult ChangeShowPricesOptionForLegoCollection(LegoCollectionViewModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!this.collectionsService.LegoCollectionExists(model.SelectedModel.CollectionId))
            {
                return this.BadRequest();
            }

            var collectionUserId = this.collectionsService.GetLegoCollectionUserId(model.SelectedModel.CollectionId);

            if (userId != collectionUserId)
            {
                return this.BadRequest();
            }

            this.collectionsService.ChangeShowPricesOptionForLegoCollection(model.SelectedModel.CollectionId);

            return this.RedirectToAction(nameof(this.LegoCollection), new { collectionId = model.SelectedModel.CollectionId });
        }

        public IActionResult MyCollections()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var model = new MyCollectionIndexViewModel();
            model.HotWheelsCollections = this.collectionsService.GetMyCollectionHotWheelsCollections(userId);
            model.LegoCollections = this.collectionsService.GetMyCollectionLegoCollections(userId);

            return this.View(model);
        }

        public IActionResult HotWheelsCollection(string collectionId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!this.collectionsService.HotWheelsCollectionExists(collectionId))
            {
                return this.BadRequest();
            }

            var collectionUserId = this.collectionsService.GetHotWheelsCollectionUserId(collectionId);

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

        public IActionResult LegoCollection(string collectionId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!this.collectionsService.LegoCollectionExists(collectionId))
            {
                return this.BadRequest();
            }

            var collectionUserId = this.collectionsService.GetLegoCollectionUserId(collectionId);

            // Collection is private AND user IS NOT owner
            if (!this.collectionsService.LegoCollectionIsPublic(collectionId) && userId != collectionUserId)
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewBag.UserIsOwner = false;
            if (userId == collectionUserId)
            {
                this.ViewBag.UserIsOwner = true;
            }

            var model = this.collectionsService.GetLegoCollectionViewModel(collectionId);

            model.AllMinifigures = this.collectionsService.GetAllLegoMinifigure();
            model.Items = this.collectionsService.GetAllLegoItems(collectionId);

            return this.View(model);
        }
    }
}
