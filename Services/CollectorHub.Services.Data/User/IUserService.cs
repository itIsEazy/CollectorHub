namespace CollectorHub.Services.Data.User
{
    using System.Threading.Tasks;

    public interface IUserService
    {
        int TotalUsersCount();

        Task<bool> UserExists(string userName);
    }
}
