namespace CollectorHub.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using CollectorHub.Web.ViewModels.Collections;

    public class MainPageIndexViewModel
    {
        public MainPageIndexViewModel()
        {
            this.TrendingCollections = new HashSet<CollectionIndexViewModel>();
        }

        public int TotalUsersCount { get; set; }

        public int TotalCollectionsCount { get; set; }

        public int TotalForumPostsCount { get; set; }

        public IEnumerable<CollectionIndexViewModel> TrendingCollections { get; set; }
    }
}
