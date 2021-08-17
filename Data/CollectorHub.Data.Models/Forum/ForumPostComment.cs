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
        [MaxLength(500)]
        public string Content { get; set; }

        public int LikesCount { get; set; }

        [Required]
        public string PostId { get; set; }

        public virtual ForumPost Post { get; set; }

        public string ParentId { get; set; }

        public virtual ForumPostComment Parent { get; set; }
    }
}
