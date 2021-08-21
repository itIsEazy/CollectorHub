namespace CollectorHub.Web.ViewModels.Administration.Info
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IndexViewModel()
        {
            this.AllUsers = new HashSet<UserIndexViewModel>();
            this.AllForumPosts = new HashSet<ForumPostIndexViewModel>();
            this.PendingForumPosts = new HashSet<ForumPostIndexViewModel>();
        }

        public string Id { get; set; }

        public IEnumerable<ForumPostIndexViewModel> PendingForumPosts { get; set; }

        public IEnumerable<UserIndexViewModel> AllUsers { get; set; }

        public IEnumerable<ForumPostIndexViewModel> AllForumPosts { get; set; }
    }
}
