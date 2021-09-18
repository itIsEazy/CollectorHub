namespace CollectorHub.Services.Data.Category
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CollectorHub.Data.Common.Repositories;
    using CollectorHub.Data.Models.Common;
    using CollectorHub.Web.ViewModels.Common;

    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> categoriesRepository;

        public CategoryService(IRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public IEnumerable<CategoryIndexViewModel> GetAllCategories()
        {
            var list = new List<CategoryIndexViewModel>();

            foreach (var category in this.categoriesRepository.AllAsNoTracking())
            {
                list.Add(new CategoryIndexViewModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    ImageUrl = category.ImageUrl,
                });
            }

            return list;
        }

        public async Task<bool> CategoryExists(string categoryId)
        {
            var category = this.categoriesRepository
                .All()
                .Where(x => x.Id == categoryId)
                .FirstOrDefault();

            if (category == null)
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
