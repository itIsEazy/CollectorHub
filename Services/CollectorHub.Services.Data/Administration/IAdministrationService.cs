namespace CollectorHub.Services.Data.Administration
{
    using System.Threading.Tasks;

    using CollectorHub.Data.Models.User;
    using CollectorHub.Web.ViewModels.Administration.Info;

    public interface IAdministrationService
    {
        Task AddNewAdminAsync(ApplicationUser user, string password);

        Task<bool> AddNewAdmin(string userId, string password);

        IndexViewModel GetIndexInfo();

        EditForumPostModel GetEditForumPostModel(string postId);

        void VerifyForumPost(string postId);

        void ShutDownForumPost(string postId);
    }
}
