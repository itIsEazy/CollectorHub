namespace CollectorHub.Web.ViewModels.Forum
{
    using System.Collections.Generic;

    public class MyPostsViewModel
    {
        public MyPostsViewModel()
        {
            this.AllPosts = new HashSet<ForumPostViewModel>();
        }

        public IEnumerable<ForumPostViewModel> AllPosts { get; set; }
    }
}
