namespace CollectorHub.Data.Models.Forum
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CollectorHub.Data.Models.Common;

    public class ForumPost : Post
    {
        public ForumPost()
        {
            this.ViewsCount = 0;
            this.LikesCount = 0;
            this.StarsCount = 0;
            this.IsVerified = false;

            this.Comments = new HashSet<ForumPostComment>();
            this.Stars = new HashSet<ForumStar>();
        }

        public bool IsVerified { get; set; }

        public int ViewsCount { get; set; }

        public int LikesCount { get; set; }

        public int StarsCount { get; set; }

        [Required]
        public string CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<ForumPostComment> Comments { get; set; }

        public virtual ICollection<ForumStar> Stars { get; set; }
    }
}
