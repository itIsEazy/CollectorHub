namespace CollectorHub.Web.ViewModels.Forum
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CollectorHub.Web.ViewModels.Common;

    public class EditForumPostViewModel
    {
        public EditForumPostViewModel()
        {
            this.Categories = new HashSet<CategoryIndexViewModel>();
        }

        public string Id { get; set; }

        public string AuthorId { get; set; }

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

        public IEnumerable<CategoryIndexViewModel> Categories { get; set; }
    }
}
