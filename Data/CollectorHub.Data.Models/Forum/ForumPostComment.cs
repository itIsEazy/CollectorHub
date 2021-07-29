namespace CollectorHub.Data.Models.Forum
{
    using System.ComponentModel.DataAnnotations;

    using CollectorHub.Data.Common.Models;
    using CollectorHub.Data.Models.User;

    public class ForumPostComment : BaseDeletableModel<string>
    {
        public ForumPostComment()
        {
            this.LikesCount = 0;
        }

        [Required]
        public ApplicationUser Author { get; set; }

        [Required]
        [MaxLength(300)]
        public string Content { get; set; }

        public int LikesCount { get; set; }

        public int PostId { get; set; }

        [Required]
        public ForumPost Post { get; set; }
    }
}