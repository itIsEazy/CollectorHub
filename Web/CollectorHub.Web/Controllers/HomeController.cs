namespace CollectorHub.Web.Controllers
{
    using System.Diagnostics;

    using CollectorHub.Services.Data.Administration;
    using CollectorHub.Services.Data.Collections;
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

        public HomeController(
            IGetHotWheelsInfoService hotWheelsInfoService,
            IAdministrationService administrationService,
            IUserService userService,
            ICollectionsService collectionsService,
            IForumService forumService)
        {
            this.hotWheelsInfoService = hotWheelsInfoService;
            this.administrationService = administrationService;
            this.userService = userService;
            this.collectionsService = collectionsService;
            this.forumService = forumService;
        }

        public IActionResult Index()
        {
            var model = new MainPageIndexViewModel();
            model.TotalUsersCount = this.userService.TotalUsersCount();
            model.TotalCollectionsCount = this.collectionsService.GetAllCollectionsCount();
            model.TotalForumPostsCount = this.forumService.TotalForumPostsCount();
            model.TrendingCollections = this.collectionsService.GetAllTrendingCollections();

            return this.View(model);
        }

        [Authorize]
        public IActionResult BecomeAdmin()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult BecomeAdmin(BecomeAdminViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (this.administrationService.AddNewAdmin(this.User.Identity.Name, input.UniquePassword).Result)
            {
                return this.RedirectToAction(nameof(this.Index));
            }
            else
            {
                return this.View(input);
            }
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
