namespace CollectorHub.Web.Controllers
{
    using System.Security.Claims;

    using CollectorHub.Services.Data.Forum;
    using CollectorHub.Services.Data.HotWheels;
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

        [HttpPost]
        public IActionResult Create(CreateForumPostInputModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.forumService.CreateForumPost(userId, model.Title, model.Content, model.ImageUrl);

            return this.RedirectToAction(nameof(this.Index));
        }

        public IActionResult Post(string postId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var model = this.forumService.GetForumPostViewModel(postId);

            return this.View(model);
        }
    }
}
