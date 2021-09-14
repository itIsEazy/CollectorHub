namespace CollectorHub.Services.Models.Forum
{
    using System.ComponentModel.DataAnnotations;

    public class ForumPostCommentInputModel
    {
        public ForumPostCommentInputModel()
        {
        }

        [Required]
        public string PostId { get; set; }

        [Required]
        [MaxLength(350)]
        public string Content { get; set; }
    }
}
