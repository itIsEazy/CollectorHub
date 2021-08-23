namespace CollectorHub.Web.Controllers
{
    using System.Diagnostics;

    using CollectorHub.Services.Data.Administration;
    using CollectorHub.Services.Data.Category;
    using CollectorHub.Services.Data.Collections;
    using CollectorHub.Services.Data.Common;
    using CollectorHub.Services.Data.Forum;
    using CollectorHub.Services.Data.HotWheels;
    using CollectorHub.Services.Data.User;
    using CollectorHub.Web.ViewModels;
    using CollectorHub.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IGetHotWheelsInfoService hotWheelsInfoService;
        private readonly IAdministrationService administrationService;
        private readonly IUserService userService;
        private readonly ICollectionsService collectionsService;
        private readonly IForumService forumService;
        private readonly ICategoryService categoryService;
        private readonly ICommonService commonService;

        public HomeController(
            IGetHotWheelsInfoService hotWheelsInfoService,
            IAdministrationService administrationService,
            IUserService userService,
            ICollectionsService collectionsService,
            IForumService forumService,
            ICategoryService categoryService,
            ICommonService commonService)
        {
            this.hotWheelsInfoService = hotWheelsInfoService;
            this.administrationService = administrationService;
            this.userService = userService;
            this.collectionsService = collectionsService;
            this.forumService = forumService;
            this.categoryService = categoryService;
            this.commonService = commonService;
        }

        public IActionResult Index()
        {
            var model = new MainPageIndexViewModel();
            model.TotalUsersCount = this.userService.TotalUsersCount();
            model.TotalCollectionsCount = this.collectionsService.GetAllCollectionsCount();
            model.TotalForumPostsCount = this.forumService.TotalForumPostsCount();

            model.Categories = this.categoryService.GetAllCategories();
            model.Sortings = this.commonService.GetAllSortings();

            model.TrendingCollections = this.collectionsService.GetTrendingCollections(null);

            return this.View(model);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
