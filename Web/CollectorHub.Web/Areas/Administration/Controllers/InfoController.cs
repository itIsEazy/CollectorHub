namespace CollectorHub.Web.Areas.Administration.Controllers
{
    using System.Security.Claims;

    using CollectorHub.Services.Data.Administration;
    using CollectorHub.Services.Data.Collections;
    using CollectorHub.Services.Data.Forum;
    using CollectorHub.Web.ViewModels.Administration.Info;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class InfoController : AdministrationController
    {
        private readonly IAdministrationService administrationService;
        private readonly IForumService forumService;

        public InfoController(
            IAdministrationService administrationService,
            IForumService forumService)
        {
            this.administrationService = administrationService;
            this.forumService = forumService;
        }

        public IActionResult Index(IndexViewModel model)
        {
            model = this.administrationService.GetIndexInfo();
            return this.View(model);
        }

        public IActionResult AddAdmin()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult AddAdmin(string s)
        {
            return this.View();
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

        public IActionResult EditForumPost(string postId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!this.forumService.PostExists(postId))
            {
                return this.BadRequest();
            }

            var model = this.administrationService.GetEditForumPostModel(postId);

            return this.View(model);
        }

        public IActionResult VerifyForumPost(string postId)
        {
            if (!this.forumService.PostExists(postId))
            {
                return this.BadRequest();
            }

            this.administrationService.VerifyForumPost(postId);

            return this.RedirectToAction("Index", "Info");
        }

        public IActionResult ShutDownForumPost(string postId)
        {
            if (!this.forumService.PostExists(postId))
            {
                return this.BadRequest();
            }

            this.administrationService.ShutDownForumPost(postId);

            return this.RedirectToAction("Index", "Info");
        }
    }
}
