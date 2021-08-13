namespace CollectorHub.Services.Data.User
{
    using System.Linq;

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
    }
}
