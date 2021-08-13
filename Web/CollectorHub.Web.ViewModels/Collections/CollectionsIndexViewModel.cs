namespace CollectorHub.Web.ViewModels.Collections
{
    using System.Collections.Generic;

    using CollectorHub.Web.ViewModels.Common;

    public class CollectionsIndexViewModel
    {
        public CollectionsIndexViewModel()
        {
            this.Categories = new HashSet<CategoryIndexViewModel>();
        }

        public IEnumerable<CategoryIndexViewModel> Categories { get; set; }

        public ICollection<CollectionIndexViewModel> TrendingCollectons { get; set; }
    }
}
