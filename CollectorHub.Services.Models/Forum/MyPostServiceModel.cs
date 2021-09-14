namespace CollectorHub.Services.Models.Forum
{
    using System.Collections.Generic;

    public class MyPostServiceModel
    {
        public MyPostServiceModel()
        {
            this.AllPosts = new HashSet<ForumPostServiceModel>();
        }

        public IEnumerable<ForumPostServiceModel> AllPosts { get; set; }
    }
}
