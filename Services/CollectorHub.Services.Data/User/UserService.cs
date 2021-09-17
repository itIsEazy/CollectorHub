namespace CollectorHub.Services.Data.User
{
    using System.Linq;
    using System.Threading.Tasks;
    using CollectorHub.Data.Common.Repositories;
    using CollectorHub.Data.Models.User;

    public class UserService : IUserService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public UserService(IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public int TotalUsersCount()
        {
            return this.usersRepository.All().Count();
        }

        public async Task<bool> UserExists(string userName)
        {
            var user = this.usersRepository.All().Where(x => x.UserName == userName).FirstOrDefault();

            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
