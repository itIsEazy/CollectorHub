﻿namespace CollectorHub.Web.Controllers
{
    using System.Security.Claims;

    using CollectorHub.Services.Data.Category;
    using CollectorHub.Services.Data.Forum;
    using CollectorHub.Services.Data.HotWheels;
    using CollectorHub.Web.ViewModels.Forum;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class ForumController : Controller
    {
        private readonly IForumService forumService;
        private readonly ICategoryService categoryService;

        public ForumController(
            IForumService forumService,
            ICategoryService categoryService)
        {
            this.forumService = forumService;
            this.categoryService = categoryService;
        }

        [AllowAnonymous]
        public IActionResult Index(string categoryId, ForumIndexViewModel model)
        {
            model.CategoryId = categoryId;
            model = this.forumService.GetIndexViewInformation(categoryId);

            return this.View(model);
        }

        public IActionResult Create()
        {
            var model = new CreateForumPostInputModel();
            model.Categories = this.categoryService.GetAllCategories();

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateForumPostInputModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            string currCreatedPostId = this.forumService.CreateForumPost(userId, model.Title, model.Content, model.ImageUrl, model.CategoryId).Result;

            return this.RedirectToAction(nameof(this.Post), new { postId = currCreatedPostId });
        }

        public IActionResult Post(string postId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var model = this.forumService.GetForumPostViewModel(postId);

            if (model == null)
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            if (model.Author.Id != userId)
            {
                this.forumService.IncreaseForumPostCount(model.Id);
            }

            return this.View(model);
        }
    }
}
