namespace CollectorHub.Services.Data.Category
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CollectorHub.Web.ViewModels.Common;

    public interface ICategoryService
    {
        IEnumerable<CategoryIndexViewModel> GetAllCategories();

        Task<bool> CategoryExists(string categoryId);
    }
}
