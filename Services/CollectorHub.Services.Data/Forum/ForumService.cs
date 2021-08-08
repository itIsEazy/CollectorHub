namespace CollectorHub.Services.Data.Forum
{
    using CollectorHub.Data.Common.Repositories;
    using CollectorHub.Data.Models.Forum;
    using CollectorHub.Services.Data.Category;
    using CollectorHub.Web.ViewModels.Forum;

    public class ForumService : IForumService
    {
        private readonly ICategoryService categoryService;
        private readonly IDeletableEntityRepository<ForumPost> forumPostsRepository;

        public ForumService(
            ICategoryService categoryService,
            IDeletableEntityRepository<ForumPost> forumPostsRepository)
        {
            this.categoryService = categoryService;
            this.forumPostsRepository = forumPostsRepository;
        }

        public ForumIndexViewModel GetIndexViewInformation()
        {
            string defaultForumPostImageUrl = "https://cdn.pixabay.com/photo/2015/10/07/12/17/post-976115_960_720.png";

            var model = new ForumIndexViewModel();

            model.Categories = this.categoryService.GetAllCategories();

            foreach (var forumPost in this.forumPostsRepository.AllAsNoTracking())
            {
                var post = new ForumPostIndexViewModel();

                post.Id = forumPost.Id;
                post.Title = forumPost.Title;
                post.Category = forumPost.Category;
                post.Date = forumPost.CreatedOn.ToString("MMMM dd, yyyy");
                post.ViewCount = forumPost.ViewsCount;
                post.StarsCount = forumPost.StarsCount;
                post.CommentCount = forumPost.Comments.Count;

                if (string.IsNullOrEmpty(forumPost.ImageUrl))
                {
                    post.ImageUrl = defaultForumPostImageUrl;
                }
                else
                {
                    post.ImageUrl = forumPost.ImageUrl;
                }
            }

            return model;
        }
    }
}
