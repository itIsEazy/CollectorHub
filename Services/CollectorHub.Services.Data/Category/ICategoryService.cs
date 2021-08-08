namespace CollectorHub.Services.Data.Category
{
    using System.Collections.Generic;

    using CollectorHub.Web.ViewModels.Common;

    public interface ICategoryService
    {
        ICollection<CategoryIndexViewModel> GetAllCategories();
    }
}
