namespace CollectorHub.Web.Api.Controllers.Forum
{
    using System.Collections.Generic;
    using System.Linq;
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
        private readonly ICategoryService categoryService;
        private readonly IUserService userService;

        public ForumPostController(
            IForumService forumService,
            ICategoryService categoryService,
            IUserService userService)
        {
            this.forumService = forumService;
            this.categoryService = categoryService;
            this.userService = userService;
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
    }
}
