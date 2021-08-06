namespace CollectorHub.Data.Models.Forum
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CollectorHub.Data.Common.Models;
    using CollectorHub.Data.Models.User;

    public class ForumPostComment : BaseDeletableModel<string>
    {
        public ForumPostComment()
        {
            this.Id = Guid.NewGuid().ToString();
            this.LikesCount = 0;
        }

        [Required]
        public string AuthorId { get; set; }

        [Required]
        public virtual ApplicationUser Author { get; set; }

        [Required]
        [MaxLength(300)]
        public string Content { get; set; }

        public int LikesCount { get; set; }

        public int PostId { get; set; }

        [Required]
        public virtual ForumPost Post { get; set; }
    }
}
