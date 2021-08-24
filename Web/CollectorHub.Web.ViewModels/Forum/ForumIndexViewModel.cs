namespace CollectorHub.Web.ViewModels.Forum
{
    using System.Collections.Generic;

    using CollectorHub.Data.Models.Common;
    using CollectorHub.Web.ViewModels.Common;

    public class ForumIndexViewModel
    {
        public ForumIndexViewModel()
        {
            this.Categories = new HashSet<CategoryIndexViewModel>();
            this.Sortings = new HashSet<SortingIndexViewModel>();

            this.TrendingPosts = new HashSet<ForumPostIndexViewModel>();
            this.PostsByCategory = new HashSet<ForumPostIndexViewModel>();
        }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public IEnumerable<CategoryIndexViewModel> Categories { get; set; }

        public IEnumerable<SortingIndexViewModel> Sortings { get; set; }

        public ICollection<ForumPostIndexViewModel> TrendingPosts { get; set; }

        public ICollection<ForumPostIndexViewModel> PostsByCategory { get; set; }

        public IndexSearchModel SearchModel { get; set; }
    }
}
