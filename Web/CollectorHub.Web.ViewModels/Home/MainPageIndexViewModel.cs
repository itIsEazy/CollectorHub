namespace CollectorHub.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using CollectorHub.Web.ViewModels.Collections.Common;

    public class MainPageIndexViewModel
    {
        public MainPageIndexViewModel()
        {
            this.TrendingCollections = new HashSet<TrendingCollectionViewModel>();
        }

        public int TotalUsersCount { get; set; }

        public int TotalCollectionsCount { get; set; }

        public int TotalForumPostsCount { get; set; }

        public IEnumerable<TrendingCollectionViewModel> TrendingCollections { get; set; }
    }
}
