namespace CollectorHub.Web.ViewModels.Forum
{
    using System.Collections.Generic;

    using CollectorHub.Web.ViewModels.Common;

    public class ForumIndexViewModel
    {
        public ForumIndexViewModel()
        {
            this.Categories = new HashSet<CategoryIndexViewModel>();
            this.TrendingPosts = new HashSet<ForumPostIndexViewModel>();
            this.PostsByCategory = new HashSet<ForumPostIndexViewModel>();
        }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public IEnumerable<CategoryIndexViewModel> Categories { get; set; }

        public ICollection<ForumPostIndexViewModel> TrendingPosts { get; set; }

        public ICollection<ForumPostIndexViewModel> PostsByCategory { get; set; }

    }
}
