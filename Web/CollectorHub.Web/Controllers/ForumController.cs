namespace CollectorHub.Web.Controllers
{
    using CollectorHub.Data;
    using CollectorHub.Services.Data.Forum;
    using CollectorHub.Web.ViewModels.Forum;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class ForumController : Controller
    {
        private readonly IForumService forumService;

        public ForumController(IForumService forumService)
        {
            this.forumService = forumService;
        }

        [AllowAnonymous]
        public IActionResult Index(ForumIndexViewModel model)
        {
            model = this.forumService.GetIndexViewInformation();

            return this.View(model);
        }

        public IActionResult Create()
        {
            return this.View();
        }
    }
}
