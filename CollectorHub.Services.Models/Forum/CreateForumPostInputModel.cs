namespace CollectorHub.Services.Models.Forum
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CollectorHub.Services.Models.Common;

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

        [Required]
        [Display(Name = "Category")]
        public string CategoryId { get; set; }

        public IEnumerable<CategoryServiceModel> Categories { get; set; }
    }
}
