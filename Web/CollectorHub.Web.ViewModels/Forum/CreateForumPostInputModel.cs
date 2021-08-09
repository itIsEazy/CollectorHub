namespace CollectorHub.Web.ViewModels.Forum
{
    using System.ComponentModel.DataAnnotations;

    using CollectorHub.Data.Models.Common;

    public class CreateForumPostInputModel
    {
        public CreateForumPostInputModel()
        {
        }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        [MaxLength(3000)]
        public string Content { get; set; }

        [Url]
        public string ImageUrl { get; set; }

        public Category Category { get; set; }
    }
}
