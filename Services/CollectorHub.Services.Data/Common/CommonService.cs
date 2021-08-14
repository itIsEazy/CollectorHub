namespace CollectorHub.Services.Data.Common
{
    using System.Collections.Generic;
    using System.Linq;

    using CollectorHub.Data.Common.Repositories;
    using CollectorHub.Data.Models.Common;
    using CollectorHub.Web.ViewModels.Common;

    public class CommonService : ICommonService
    {
        private readonly IDeletableEntityRepository<Sorting> sortingRepository;

        public CommonService(IDeletableEntityRepository<Sorting> sortingRepository)
        {
            this.sortingRepository = sortingRepository;
        }

        public IEnumerable<SortingIndexViewModel> GetAllSortings()
        {
            var list = new List<SortingIndexViewModel>();

            foreach (var sort in this.sortingRepository.All().OrderBy(x => x.Id))
            {
                list.Add(new SortingIndexViewModel
                {
                    Id = sort.Id,
                    Name = sort.Name,
                });
            }

            return list;
        }
    }
}
