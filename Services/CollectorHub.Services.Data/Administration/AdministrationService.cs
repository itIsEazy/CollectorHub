namespace CollectorHub.Services.Data.Administration
{
    using System.Linq;
    using System.Threading.Tasks;

    using CollectorHub.Data.Common.Repositories;
    using CollectorHub.Data.Models.User;
    using Microsoft.AspNetCore.Identity;

    public class AdministrationService : IAdministrationService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<ApplicationRole> rolesRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public AdministrationService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<ApplicationRole> rolesRepository,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            this.usersRepository = usersRepository;
            this.rolesRepository = rolesRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<bool> AddNewAdmin(string userName, string password)
        {
            var user = this.usersRepository
                .All()
                .ToList()
                .Where(name => name.UserName == userName)
                .FirstOrDefault();

            if (password != "uniquepassword1234")
            {
                return false;
            }

            if (!await this.roleManager.RoleExistsAsync("Admin"))
            {
                await this.roleManager.CreateAsync(new ApplicationRole
                {
                    Name = "Admin",
                });
            }

            await this.userManager.AddToRoleAsync(user, "Admin");

            return true;
        }
    }
}
