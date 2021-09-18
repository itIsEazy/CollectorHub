namespace CollectorHub.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using CollectorHub.Services.Data.Category;
    using CollectorHub.Services.Data.Forum;
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
        public async Task<IActionResult> Index(string categoryId, ForumIndexViewModel model)
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

            if (currCategoryId != null && !await this.categoryService.CategoryExists(currCategoryId))
            {
                return this.BadRequest();
            }

            model = this.forumService.GetIndexViewInformation(currCategoryId, searchInput, sortingid);

            return this.View(model);
        }

        public IActionResult Create()
        {
            var model = new CreateForumPostInputModel();
            model.Categories = this.categoryService.GetAllCategories();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateForumPostInputModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!await this.categoryService.CategoryExists(model.CategoryId))
            {
                return this.BadRequest();
            }

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

            this.ViewBag.UserIsAuthor = false;

            if (!this.forumService.PostExists(postId))
            {
                return this.BadRequest();
            }

            var model = this.forumService.GetForumPostViewModel(postId);

            if (userId == model.AuthorId)
            {
                this.ViewBag.UserIsAuthor = true;
            }

            if (model == null)
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            if (model.AuthorId != userId)
            {
                this.forumService.IncreaseForumPostCount(model.Id);
            }

            return this.View(model);
        }

        public IActionResult MyPosts()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var model = new MyPostsViewModel();

            model.AllPosts = this.forumService.GetMyPostsAllPosts(userId);

            return this.View(model);
        }

        public IActionResult EditPost(string postId)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!this.forumService.PostExists(postId))
            {
                return this.BadRequest();
            }

            var model = this.forumService.GetEditForumPostViewModel(postId);

            if (model.AuthorId != userId)
            {
                return this.BadRequest();
            }

            model.Categories = this.categoryService.GetAllCategories();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditPost(EditForumPostViewModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!this.forumService.PostExists(model.Id))
            {
                return this.BadRequest();
            }

            if (!await this.categoryService.CategoryExists(model.CategoryId))
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

            return this.RedirectToAction(nameof(this.MyPosts));
        }

        public IActionResult AddCommentToPost(ForumPostViewModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!this.forumService.PostExists(model.CommentInput.PostId))
            {
                return this.BadRequest();
            }

            //// needs to add CUSTOM validation method because i m using model inside model and ModelState.IsValid() is not working

            this.forumService.AddCommentToPost(model.CommentInput.PostId, userId, model.CommentInput.Content);

            return this.RedirectToAction(nameof(this.Post), new { postId = model.CommentInput.PostId });
        }
    }
}
