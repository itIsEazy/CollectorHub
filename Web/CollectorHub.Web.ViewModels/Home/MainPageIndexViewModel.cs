namespace CollectorHub.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using CollectorHub.Web.ViewModels.Collections.Common;
    using CollectorHub.Web.ViewModels.Common;
    using CollectorHub.Web.ViewModels.Forum;

    public class MainPageIndexViewModel
    {
        public MainPageIndexViewModel()
        {
            this.Categories = new HashSet<CategoryIndexViewModel>();
            this.Sortings = new HashSet<SortingIndexViewModel>();

            this.TrendingCollections = new HashSet<TrendingCollectionViewModel>();
        }

        public int TotalUsersCount { get; set; }

        public int TotalCollectionsCount { get; set; }

        public int TotalForumPostsCount { get; set; }

        public IEnumerable<CategoryIndexViewModel> Categories { get; set; }

        public IEnumerable<SortingIndexViewModel> Sortings { get; set; }

        public IEnumerable<TrendingCollectionViewModel> TrendingCollections { get; set; }

        public IndexSearchModel SearchModel { get; set; }
    }
}
