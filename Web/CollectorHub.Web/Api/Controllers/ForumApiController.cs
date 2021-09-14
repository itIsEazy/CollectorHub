namespace CollectorHub.Web.Api.Controllers
{
    using CollectorHub.Services.Data.Category;
    using CollectorHub.Services.Data.Forum;
    using CollectorHub.Services.Models.Forum;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/forum")]
    public class ForumApiController : ControllerBase
    {
        private readonly IForumService forumService;
        private readonly ICategoryService categoryService;

        public ForumApiController(
            IForumService forumService,
            ICategoryService categoryService)
        {
            this.forumService = forumService;
            this.categoryService = categoryService;
        }

        public ForumPostServiceModel GetForumPost()
        {
            string id = "65a92dea-f14f-4b22-aa8c-9d262677bacf";
            return this.forumService.GetForumPostServiceModel(id);
        }

    }
}
