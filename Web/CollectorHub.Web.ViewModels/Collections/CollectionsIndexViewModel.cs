namespace CollectorHub.Web.ViewModels.Collections
{
    using System.Collections.Generic;

    using CollectorHub.Web.ViewModels.Collections.Common;
    using CollectorHub.Web.ViewModels.Common;

    public class CollectionsIndexViewModel
    {
        public CollectionsIndexViewModel()
        {
            this.Sortings = new HashSet<SortingIndexViewModel>();
            this.Categories = new HashSet<CategoryIndexViewModel>();
            this.TrendingCollectons = new HashSet<TrendingCollectionViewModel>();
        }

        public IEnumerable<SortingIndexViewModel> Sortings { get; set; }

        public IEnumerable<CategoryIndexViewModel> Categories { get; set; }

        public IEnumerable<TrendingCollectionViewModel> TrendingCollectons { get; set; }

        public CollectionsIndexSearchModel SearchModel { get; set; }
    }
}
