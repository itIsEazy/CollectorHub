namespace CollectorHub.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using CollectorHub.Services.Data.Administration;
    using CollectorHub.Services.Data.Category;
    using CollectorHub.Services.Data.Collections;
    using CollectorHub.Services.Data.Common;
    using CollectorHub.Services.Data.Forum;
    using CollectorHub.Services.Data.User;
    using CollectorHub.Web.ViewModels;
    using CollectorHub.Web.ViewModels.Administration.Info;
    using CollectorHub.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class HomeController : BaseController
    {
        private readonly IAdministrationService administrationService;
        private readonly IUserService userService;
        private readonly ICollectionsService collectionsService;
        private readonly IForumService forumService;
        private readonly ICategoryService categoryService;
        private readonly ICommonService commonService;

        public HomeController(
            IAdministrationService administrationService,
            IUserService userService,
            ICollectionsService collectionsService,
            IForumService forumService,
            ICategoryService categoryService,
            ICommonService commonService)
        {
            this.administrationService = administrationService;
            this.userService = userService;
            this.collectionsService = collectionsService;
            this.forumService = forumService;
            this.categoryService = categoryService;
            this.commonService = commonService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var model = new MainPageIndexViewModel();
            model.TotalUsersCount = this.userService.TotalUsersCount();
            model.TotalCollectionsCount = this.collectionsService.GetAllCollectionsCount();
            model.TotalForumPostsCount = this.forumService.TotalForumPostsCount();

            model.Categories = this.categoryService.GetAllCategories();
            model.Sortings = this.commonService.GetAllSortings();

            model.TrendingCollections = this.collectionsService.GetAllCollections();

            return this.View(model);
        }

        public IActionResult BecomeAdmin()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult BecomeAdmin(BecomeAdminViewModel input)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            if (this.administrationService.AddNewAdmin(userId, input.UniquePassword).Result)
            {
                return this.Redirect("https://localhost:5001/");
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
