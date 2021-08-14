namespace CollectorHub.Web.ViewModels.Forum
{
    using System.Collections.Generic;

    using CollectorHub.Data.Models.Common;
    using CollectorHub.Web.ViewModels.Common;

    public class IndexViewModel
    {
        public IndexViewModel()
        {
            this.Categories = new HashSet<CategoryIndexViewModel>();
            this.TrendingPosts = new HashSet<ForumPostIndexViewModel>();
            this.PostsByCategory = new HashSet<ForumPostIndexViewModel>();
            this.Sortings = new HashSet<SortingIndexViewModel>();
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
