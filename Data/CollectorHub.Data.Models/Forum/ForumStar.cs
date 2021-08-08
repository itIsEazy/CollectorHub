namespace CollectorHub.Data.Models.Forum
{
    using System.Collections.Generic;

    using CollectorHub.Data.Models.Common;

    public class ForumStar : RateStar
    {
        public ForumStar()
        {
            this.Posts = new HashSet<ForumPost>();
        }

        public virtual ICollection<ForumPost> Posts { get; set; }
    }
}
