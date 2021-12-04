namespace CollectorHub.Web.Api.Controllers
{
    using CollectorHub.Services.Data.Category;
    using CollectorHub.Services.Data.Forum;
    using CollectorHub.Services.Models.Forum;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class ForumController : ControllerBase
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

        // /api/forum/GetAllPosts
        [Route(nameof(GetAllPosts))]
        [HttpGet]
        public ForumPostServiceModel GetAllPosts()
        {
            string id = "65a92dea-f14f-4b22-aa8c-9d262677bacf";
            return this.forumService.GetForumPostServiceModel(id);
        }

        [HttpPut]
        public ForumPostServiceModel GetAllPosts2()
        {
            string id = "65a92dea-f14f-4b22-aa8c-9d262677bacf";
            return this.forumService.GetForumPostServiceModel(id);
        }

        [HttpPost]
        public ForumPostServiceModel GetAllPosts22(ForumPostServiceModel model)
        {
            return model;
        }
    }
}
