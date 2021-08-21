namespace CollectorHub.Services.Data.Administration
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CollectorHub.Common;
    using CollectorHub.Data.Common.Repositories;
    using CollectorHub.Data.Models.Forum;
    using CollectorHub.Data.Models.User;
    using CollectorHub.Services.Data.Collections;
    using CollectorHub.Services.Data.Forum;
    using CollectorHub.Web.ViewModels.Administration.Info;
    using Microsoft.AspNetCore.Identity;

    public class AdministrationService : IAdministrationService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<ApplicationRole> rolesRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IDeletableEntityRepository<ForumPost> forumPostsRepository;

        public AdministrationService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<ApplicationRole> rolesRepository,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IDeletableEntityRepository<ForumPost> forumPostsRepository)
        {
            this.usersRepository = usersRepository;
            this.rolesRepository = rolesRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.forumPostsRepository = forumPostsRepository;
        }

        public async Task AddNewAdminAsync(ApplicationUser user, string password)
        {
            if (user == null)
            {
                return;
            }

            if (password != "uniquepassword1234")
            {
                return;
            }

            if (!this.userManager.IsInRoleAsync(user, GlobalConstants.AdministratorRoleName).Result)
            {
                return;
            }

            if (!await this.roleManager.RoleExistsAsync(GlobalConstants.AdministratorRoleName))
            {
                await this.roleManager.CreateAsync(new ApplicationRole
                {
                    Name = "Admin",
                });
            }

            await this.userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);

            return;
        }

        public async Task<bool> AddNewAdmin(string userId, string password)
        {
            var user = this.usersRepository
                .All()
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            if (user == null)
            {
                return false;
            }
            else
            {
                await this.AddNewAdminAsync(user, password);
                return true;
            }
        }

        public IndexViewModel GetIndexInfo()
        {
            var model = new IndexViewModel();

            model.PendingForumPosts = this.GetAllPendingForumPosts();
            model.AllUsers = this.GetAllUsers();
            model.AllForumPosts = this.GetAllForumPosts();

            return model;
        }

        public EditForumPostModel GetEditForumPostModel(string postId)
        {
            var model = new EditForumPostModel();

            var post = this.forumPostsRepository
                .All()
                .Where(x => x.Id == postId)
                .Select(x => new
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    ImageUrl = x.ImageUrl,
                    IsVerified = x.IsVerified,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name,
                })
                .FirstOrDefault();

            model.Id = post.Id;
            model.Title = post.Title;
            model.Content = post.Content;
            model.ImageUrl = post.ImageUrl;
            model.IsVerified = post.IsVerified;
            model.CategoryId = post.CategoryId;
            model.CategoryName = post.CategoryName;

            return model;
        }

        public void VerifyForumPost(string postId)
        {
            var post = this.forumPostsRepository
                .All()
                .Where(x => x.Id == postId)
                .FirstOrDefault();

            post.IsVerified = true;
            this.forumPostsRepository.SaveChanges();
        }

        public void ShutDownForumPost(string postId)
        {
            var post = this.forumPostsRepository
                .All()
                .Where(x => x.Id == postId)
                .FirstOrDefault();

            post.IsVerified = false;
            this.forumPostsRepository.SaveChanges();
        }

        private List<ForumPostIndexViewModel> GetAllPendingForumPosts()
        {
            var posts = new List<ForumPostIndexViewModel>();

            foreach (var post in this.forumPostsRepository.All().Where(x => x.IsVerified == false))
            {
                posts.Add(new ForumPostIndexViewModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    ImageUrl = post.ImageUrl,
                    UserId = post.AuthorId,
                    DateCreated = post.CreatedOn.ToString("MM/dd/yyyy H:mm"),
                });
            }

            posts = posts.OrderByDescending(x => x.DateCreated).ToList();

            return posts;
        }

        private List<UserIndexViewModel> GetAllUsers()
        {
            var users = new List<UserIndexViewModel>();

            foreach (var user in this.usersRepository.All())
            {
                users.Add(new UserIndexViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    DateCreated = user.CreatedOn.ToString("MM/dd/yyyy H:mm"),
                });
            }

            users = users.OrderByDescending(x => x.DateCreated).ToList();

            return users;
        }

        private List<ForumPostIndexViewModel> GetAllForumPosts()
        {
            var posts = new List<ForumPostIndexViewModel>();

            foreach (var post in this.forumPostsRepository.All().Where(x => x.IsVerified == true))
            {
                posts.Add(new ForumPostIndexViewModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    ImageUrl = post.ImageUrl,
                    UserId = post.AuthorId,
                    DateCreated = post.CreatedOn.ToString("MM/dd/yyyy H:mm"),
                });
            }

            posts = posts.OrderByDescending(x => x.DateCreated).ToList();

            return posts;
        }
    }
}
