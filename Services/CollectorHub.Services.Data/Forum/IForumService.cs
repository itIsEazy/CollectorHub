namespace CollectorHub.Services.Data.Forum
{
    using System.Threading.Tasks;

    using CollectorHub.Web.ViewModels.Forum;

    public interface IForumService
    {
        int TotalForumPostsCount();

        ForumIndexViewModel GetIndexViewInformation(string categoryId);

        ForumPostViewModel GetForumPostViewModel(string postId);

        Task<string> CreateForumPost(string userId, string title, string content, string imageUrl, string categoryId);

        void IncreaseForumPostCount(string postId);
    }
}
