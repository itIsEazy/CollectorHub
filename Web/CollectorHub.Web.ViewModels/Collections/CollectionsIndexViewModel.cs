namespace CollectorHub.Web.ViewModels.Collections
{
    using System.Collections.Generic;

    using CollectorHub.Web.ViewModels.Common;

    public class CollectionsIndexViewModel
    {
        public CollectionsIndexViewModel()
        {
            this.Sortings = new HashSet<SortingIndexViewModel>();
            this.Categories = new HashSet<CategoryIndexViewModel>();
            this.TrendingCollectons = new HashSet<CollectionIndexViewModel>();
        }

        public IEnumerable<SortingIndexViewModel> Sortings { get; set; }

        public IEnumerable<CategoryIndexViewModel> Categories { get; set; }

        public IEnumerable<CollectionIndexViewModel> TrendingCollectons { get; set; }

        public CollectionsIndexSearchModel SearchModel { get; set; }
    }
}
