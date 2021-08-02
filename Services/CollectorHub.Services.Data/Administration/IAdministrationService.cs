namespace CollectorHub.Services.Data.Administration
{
    using System.Threading.Tasks;

    public interface IAdministrationService
    {
        Task<bool> AddNewAdmin(string userName, string password);
    }
}
