namespace CollectorHub.Web.Api.Controllers.Forum
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using CollectorHub.Services.Data.Category;
    using CollectorHub.Services.Data.Forum;
    using CollectorHub.Services.Data.User;
    using CollectorHub.Services.Models.Forum;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class ForumPostController : ControllerBase
    {
        private readonly IForumService forumService;
        private readonly IUserService userService;
        private readonly ICategoryService categoryService;

        public ForumPostController(
            IForumService forumService,
            IUserService userService,
            ICategoryService categoryService)
        {
            this.forumService = forumService;
            this.userService = userService;
            this.categoryService = categoryService;
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<IEnumerable<ForumPostServiceModel>>> GetUserPosts(string username)
        {
            if (await this.userService.UserExists(username) == false)
            {
                return this.NotFound();
            }

            var posts = await this.forumService.GetUserPosts(username);

            if (posts.Any())
            {
                return this.Ok(posts);
            }
            else
            {
                return this.NotFound();
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post(CreateForumPostInputModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!await this.categoryService.CategoryExists(model.CategoryId))
            {
                return this.BadRequest();
            }

            string currCreatedPostId = await this.forumService.CreateForumPost(userId, model.Title, model.Content, model.ImageUrl, model.CategoryId);

            return this.CreatedAtAction("Post", new { id = currCreatedPostId }, model);
        }
    }
}
