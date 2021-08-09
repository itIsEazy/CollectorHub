namespace CollectorHub.Services.Data.Forum
{
    using System.Linq;
    using System.Threading.Tasks;

    using CollectorHub.Data.Common.Repositories;
    using CollectorHub.Data.Models.Forum;
    using CollectorHub.Data.Models.User;
    using CollectorHub.Services.Data.Category;
    using CollectorHub.Web.ViewModels.Forum;

    public class ForumService : IForumService
    {
        private readonly ICategoryService categoryService;
        private readonly IDeletableEntityRepository<ForumPost> forumPostsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> allUsers;

        public ForumService(
            ICategoryService categoryService,
            IDeletableEntityRepository<ForumPost> forumPostsRepository,
            IDeletableEntityRepository<ApplicationUser> allUsers)
        {
            this.categoryService = categoryService;
            this.forumPostsRepository = forumPostsRepository;
            this.allUsers = allUsers;
        }

        public ForumIndexViewModel GetIndexViewInformation()
        {
            string defaultForumPostImageUrl = "https://cdn.pixabay.com/photo/2015/10/07/12/17/post-976115_960_720.png";

            var model = new ForumIndexViewModel();

            model.Categories = this.categoryService.GetAllCategories();

            foreach (var forumPost in this.forumPostsRepository.All())
            {
                var post = new ForumPostIndexViewModel();

                post.Id = forumPost.Id;
                post.Title = forumPost.Title;
                post.Content = forumPost.Content;
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

                model.ForumPosts.Add(post);
            }

            return model;
        }

        public ForumPostViewModel GetForumPostViewModel(string postId)
        {
            var model = new ForumPostViewModel();

            var postFromDatabase = this.forumPostsRepository.All().Where(x => x.Id == postId).FirstOrDefault();

            var postAuthour = this.allUsers.All().Where(x => x.Id == postFromDatabase.AuthorId).FirstOrDefault();

            model.Id = postFromDatabase.Id;
            model.Author = postAuthour;
            model.Title = postFromDatabase.Title;
            model.Content = postFromDatabase.Content;
            model.ImageUrl = postFromDatabase.ImageUrl;

            model.LikesCount = postFromDatabase.LikesCount;
            model.StarsCount = postFromDatabase.StarsCount;
            model.ViewsCount = postFromDatabase.ViewsCount;

            model.Comments = postFromDatabase.Comments;
            model.Stars = postFromDatabase.Stars;

            return model;
        }

        // VERY VERY VERY VERY VERY VRY VEYYEYEYGHALIEUGAEUGOIEAG Add Gategory by passing daerektly Category or category id most probably
        // VERY VERY VERY VERY VERY VRY VEYYEYEYGHALIEUGAEUGOIEAG Add Gategory by passing daerektly Category or category id most probably
        // VERY VERY VERY VERY VERY VRY VEYYEYEYGHALIEUGAEUGOIEAG Add Gategory by passing daerektly Category or category id most probably
        // VERY VERY VERY VERY VERY VRY VEYYEYEYGHALIEUGAEUGOIEAG Add Gategory by passing daerektly Category or category id most probably
        public async Task CreateForumPost(string userId, string title, string content, string imageUrl)
        {
            var post = new ForumPost();

            var user = this.allUsers.All().Where(x => x.Id == userId).FirstOrDefault();

            post.Author = user;
            post.AuthorId = user.Id;
            post.Title = title;
            post.Content = content;
            post.ImageUrl = imageUrl;

            user.ForumPosts.Add(post);

            Task.WaitAll(this.forumPostsRepository.AddAsync(post));
            Task.WaitAll(this.allUsers.SaveChangesAsync());

            await this.forumPostsRepository.SaveChangesAsync();
        }
    }
}
