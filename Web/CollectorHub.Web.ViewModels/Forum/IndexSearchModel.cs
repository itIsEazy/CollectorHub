namespace CollectorHub.Web.ViewModels.Forum
{
    using System.ComponentModel.DataAnnotations;

    public class IndexSearchModel
    {
        public IndexSearchModel()
        {
        }

        [Display(Name ="Category")]
        public string CategoryId { get; set; }

        [Display(Name = "Search")]
        public string SearchInput { get; set; }

        [Required]
        [Display(Name = "Sort")]
        public int SortingId { get; set; }
    }
}
