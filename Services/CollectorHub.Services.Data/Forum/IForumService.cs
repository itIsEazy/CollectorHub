namespace CollectorHub.Services.Data.Forum
{
    using CollectorHub.Web.ViewModels.Forum;

    public interface IForumService
    {
        ForumIndexViewModel GetIndexViewInformation();
    }
}
