namespace CollectorHub.Services.Data.Forum
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CollectorHub.Services.Models.Forum;
    using CollectorHub.Web.ViewModels.Forum;

    public interface IForumService
    {
        int TotalForumPostsCount();

        ForumIndexViewModel GetIndexViewInformation(string categoryId, string searchInput, int sortingId);

        ForumPostServiceModel GetForumPostServiceModel(string postId);

        ForumPostViewModel GetForumPostViewModel(string postId);

        Task<string> CreateForumPost(string userId, string title, string content, string imageUrl, string categoryId);

        Task<IEnumerable<ForumPostServiceModel>> GetUserPosts(string userName);

        void EditForumPost(string postId, string title, string content, string imageUrl, string categoryId);

        IEnumerable<ForumPostViewModel> GetMyPostsAllPosts(string userId);

        EditForumPostViewModel GetEditForumPostViewModel(string postId);

        void IncreaseForumPostCount(string postId);

        Task AddCommentToPost(string postId, string authorId, string content);

        bool PostExists(string postId);

        string GetAuthorId(string postId);
    }
}
