namespace CollectorHub.Web.Areas.Administration.Controllers
{
    using System.Security.Claims;

    using CollectorHub.Services.Data.Administration;
    using CollectorHub.Services.Data.Category;
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
        private readonly ICategoryService categoryService;

        public InfoController(
            IAdministrationService administrationService,
            IForumService forumService,
            ICategoryService categoryService)
        {
            this.administrationService = administrationService;
            this.forumService = forumService;
            this.categoryService = categoryService;
        }

        public IActionResult Index(IndexViewModel model)
        {
            model = this.administrationService.GetIndexInfo();
            return this.View(model);
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

        [HttpPost]
        public IActionResult EditForumPost(EditForumPostModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!this.forumService.PostExists(model.Id))
            {
                return this.BadRequest();
            }

            if (!this.categoryService.CategoryExists(model.CategoryId))
            {
                return this.BadRequest();
            }

            var postAuthorId = this.forumService.GetAuthorId(model.Id);

            if (postAuthorId != userId)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.forumService.EditForumPost(model.Id, model.Title, model.Content, model.ImageUrl, model.CategoryId);

            return this.RedirectToAction(nameof(this.Index));
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
