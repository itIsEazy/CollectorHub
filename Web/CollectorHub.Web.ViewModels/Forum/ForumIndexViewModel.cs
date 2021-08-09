namespace CollectorHub.Web.ViewModels.Forum
{
    using System.Collections.Generic;

    using CollectorHub.Web.ViewModels.Common;

    public class ForumIndexViewModel
    {
        public ForumIndexViewModel()
        {
            this.Categories = new HashSet<CategoryIndexViewModel>();
            this.ForumPosts = new HashSet<ForumPostIndexViewModel>();
        }

        public ICollection<CategoryIndexViewModel> Categories { get; set; }

        public ICollection<ForumPostIndexViewModel> ForumPosts { get; set; }
    }
}
