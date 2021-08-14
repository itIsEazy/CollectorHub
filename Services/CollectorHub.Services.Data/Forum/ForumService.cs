﻿namespace CollectorHub.Services.Data.Forum
{
    using System.Collections.Generic;
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

        public int TotalForumPostsCount()
        {
            return this.forumPostsRepository.All().Count();
        }

        public ForumIndexViewModel GetIndexViewInformation(string categoryId)
        {
            string defaultForumPostImageUrl = "https://cdn.pixabay.com/photo/2015/10/07/12/17/post-976115_960_720.png";

            var model = new ForumIndexViewModel();

            model.Categories = this.categoryService.GetAllCategories();

            var allPosts = this.forumPostsRepository.All();

            if (categoryId != null)
            {
                allPosts = this.forumPostsRepository.All().Where(x => x.CategoryId == categoryId);
                model.CategoryName = this.categoryService
                    .GetAllCategories()
                    .Where(x => x.Id == categoryId)
                    .Select(x => x.Name)
                    .FirstOrDefault()
                    .ToString();
            }

            foreach (var forumPost in allPosts)
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

            model.Comments = postFromDatabase.Comments;
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
    }
}
