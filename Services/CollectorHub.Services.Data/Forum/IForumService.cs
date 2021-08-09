namespace CollectorHub.Services.Data.Forum
{
    using System.Threading.Tasks;

    using CollectorHub.Web.ViewModels.Forum;

    public interface IForumService
    {
        ForumIndexViewModel GetIndexViewInformation();

        Task CreateForumPost(string userId, string title, string content, string imageUrl);
    }
}
