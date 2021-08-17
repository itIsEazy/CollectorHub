namespace CollectorHub.Services.Data.Forum
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CollectorHub.Data.Common.Repositories;
    using CollectorHub.Data.Models.Forum;
    using CollectorHub.Data.Models.User;
    using CollectorHub.Services.Data.Category;
    using CollectorHub.Services.Data.Common;
    using CollectorHub.Web.ViewModels.Forum;

    public class ForumService : IForumService
    {
        private readonly ICategoryService categoryService;
        private readonly ICommonService commonService;
        private readonly IDeletableEntityRepository<ForumPost> forumPostsRepository;
        private readonly IDeletableEntityRepository<ForumPostComment> forumPostCommentsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> allUsers;

        public ForumService(
            ICategoryService categoryService,
            ICommonService commonService,
            IDeletableEntityRepository<ForumPost> forumPostsRepository,
            IDeletableEntityRepository<ForumPostComment> forumPostCommentsRepository,
            IDeletableEntityRepository<ApplicationUser> allUsers)
        {
            this.categoryService = categoryService;
            this.commonService = commonService;
            this.forumPostsRepository = forumPostsRepository;
            this.forumPostCommentsRepository = forumPostCommentsRepository;
            this.allUsers = allUsers;
        }

        public int TotalForumPostsCount()
        {
            return this.forumPostsRepository.All().Count();
        }

        public IndexViewModel GetIndexViewInformation(string categoryId, string searchInput, int sortingId)
        {
            string defaultForumPostImageUrl = "https://cdn.pixabay.com/photo/2015/10/07/12/17/post-976115_960_720.png";

            var model = new IndexViewModel();

            model.Categories = this.categoryService.GetAllCategories();
            model.Sortings = this.commonService.GetAllSortings();

            var allPosts = this.forumPostsRepository.All().ToList();

            //// if User is getting collection by CATEGORY Button
            if (categoryId != null)
            {
                allPosts = this.forumPostsRepository.All().Where(x => x.CategoryId == categoryId).ToList();
                model.CategoryName = this.categoryService
                    .GetAllCategories()
                    .Where(x => x.Id == categoryId)
                    .Select(x => x.Name)
                    .FirstOrDefault()
                    .ToString();
            }

            //// if User has Typed somethign in searchInput
            if (searchInput != null)
            {
                var searchedList = new List<ForumPost>();
                List<string> words = searchInput.Split().ToList();

                foreach (var post in allPosts)
                {
                    int wordMatchedCount = 0;

                    bool removeCurrentPost = true;

                    foreach (var word in words)
                    {
                        if (post.Title.Contains(word))
                        {
                            removeCurrentPost = false;
                            wordMatchedCount += 1;
                        }

                        if (post.Content.Contains(word))
                        {
                            removeCurrentPost = false;
                            wordMatchedCount += 1;
                        }

                        if (wordMatchedCount >= 3)
                        {
                            break;
                        }
                    }

                    if (!removeCurrentPost)
                    {
                        searchedList.Add(post);
                    }
                }

                allPosts = searchedList;
            }

            //// if User is using sorting option
            if (sortingId != 0)
            {
                var sorting = this.commonService.GetAllSortings().Where(x => x.Id == sortingId).FirstOrDefault();

                if (sorting.Name == "Newest")
                {
                    allPosts = allPosts.OrderByDescending(x => x.CreatedOn).ToList();
                }
                else if (sorting.Name == "Oldest")
                {
                    allPosts = allPosts.OrderBy(x => x.CreatedOn).ToList();
                }
                else if (sorting.Name == "Most viewed")
                {
                    allPosts = allPosts.OrderByDescending(x => x.ViewsCount).ToList();
                }
                else if (sorting.Name == "Less viewed")
                {
                    allPosts = allPosts.OrderBy(x => x.ViewsCount).ToList();
                }
            }

            foreach (var forumPost in allPosts)
            {
                var post = new ForumPostIndexViewModel();

                post.Id = forumPost.Id;
                post.Title = forumPost.Title;
                post.OwnerUserName = forumPost.Author.UserName;
                post.Content = forumPost.Content;
                post.Category = forumPost.Category;
                post.Date = forumPost.CreatedOn.ToString("MMMM dd, yyyy");
                post.ViewCount = forumPost.ViewsCount;
                post.StarsCount = forumPost.StarsCount;
                post.CommentCount = forumPost.Comments.Count;
                post.Comments = this.GetPostComments(forumPost.Id);

                if (string.IsNullOrEmpty(forumPost.ImageUrl))
                {
                    post.ImageUrl = defaultForumPostImageUrl;
                }
                else
                {
                    post.ImageUrl = forumPost.ImageUrl;
                }

                if (categoryId != null)
                {
                    model.PostsByCategory.Add(post);
                }
                else
                {
                    model.TrendingPosts.Add(post);
                }
            }

            return model;
        }

        public ForumPostViewModel GetForumPostViewModel(string postId)
        {
            var model = new ForumPostViewModel();

            var postFromDatabase = this.forumPostsRepository.All().Where(x => x.Id == postId).FirstOrDefault();

            if (postFromDatabase == null)
            {
                return null;
            }

            var postAuthour = this.allUsers.All().Where(x => x.Id == postFromDatabase.AuthorId).FirstOrDefault();

            model.Id = postFromDatabase.Id;
            model.Author = postAuthour;
            model.Title = postFromDatabase.Title;
            model.Content = postFromDatabase.Content;
            model.ImageUrl = postFromDatabase.ImageUrl;

            model.LikesCount = postFromDatabase.LikesCount;
            model.StarsCount = postFromDatabase.StarsCount;
            model.ViewsCount = postFromDatabase.ViewsCount;

            model.Comments = this.GetPostComments(postId);
            model.Stars = postFromDatabase.Stars;

            return model;
        }

        // VERY VERY VERY VERY VERY VRY VEYYEYEYGHALIEUGAEUGOIEAG Add Gategory by passing daerektly Category or category id most probably
        // VERY VERY VERY VERY VERY VRY VEYYEYEYGHALIEUGAEUGOIEAG Add Gategory by passing daerektly Category or category id most probably
        // VERY VERY VERY VERY VERY VRY VEYYEYEYGHALIEUGAEUGOIEAG Add Gategory by passing daerektly Category or category id most probably
        // VERY VERY VERY VERY VERY VRY VEYYEYEYGHALIEUGAEUGOIEAG Add Gategory by passing daerektly Category or category id most probably
        public async Task<string> CreateForumPost(string userId, string title, string content, string imageUrl, string categoryId)
        {
            string defaultForumPostImageUrl = "https://cdn.pixabay.com/photo/2015/10/07/12/17/post-976115_960_720.png";

            var post = new ForumPost();

            var user = this.allUsers.All().Where(x => x.Id == userId).FirstOrDefault();

            post.Author = user;
            post.AuthorId = user.Id;
            post.Title = title;
            post.Content = content;
            post.CategoryId = categoryId;

            if (string.IsNullOrEmpty(imageUrl))
            {
                post.ImageUrl = defaultForumPostImageUrl;
            }
            else
            {
                post.ImageUrl = imageUrl;
            }

            user.ForumPosts.Add(post);

            Task.WaitAll(this.forumPostsRepository.AddAsync(post));
            Task.WaitAll(this.allUsers.SaveChangesAsync());

            await this.forumPostsRepository.SaveChangesAsync();

            return post.Id;
        }

        // NICE TO HAVE u may collect all the users that have seen this post in some hashset (this information can be used later in the game for statistics ML.NET :))
        public void IncreaseForumPostCount(string postId)
        {
            var post = this.forumPostsRepository.All().Where(x => x.Id == postId).FirstOrDefault();

            post.ViewsCount += 1;

            this.forumPostsRepository.SaveChanges();

            // await this.forumPostsRepository.SaveChangesAsync();
        }

        public async Task AddCommentToPost(string postId, string authorId, string content)
        {
            var comment = new ForumPostComment();
            comment.AuthorId = authorId;
            comment.Content = content;
            comment.PostId = postId;

            await this.forumPostCommentsRepository.AddAsync(comment);

            this.forumPostCommentsRepository.SaveChanges();
        }

        private IEnumerable<ForumPostCommentViewModel> GetPostComments(string postId)
        {
            var list = new List<ForumPostCommentViewModel>();

            var allComments = this.forumPostCommentsRepository
                .All()
                .Where(x => x.PostId == postId)
                .OrderBy(x => x.CreatedOn)
                .Select(x => new
                {
                    Id = x.Id,
                    Content = x.Content,
                    AuthorUserName = x.Author.UserName,
                });

            foreach (var comment in allComments)
            {
                list.Add(new ForumPostCommentViewModel
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    AuthorUserName = comment.AuthorUserName,
                });
            }

            return list;
        }
    }
}
