namespace CollectorHub.Web.ViewModels.Collections
{
    using System.ComponentModel.DataAnnotations;

    public class CollectionsIndexSearchModel
    {
        public CollectionsIndexSearchModel()
        {
        }

        [Display(Name = "Category")]
        public string CategoryId { get; set; }

        [Display(Name = "Search")]
        public string SearchInput { get; set; }

        [Required]
        [Display(Name = "Sort")]
        public int SortingId { get; set; }
    }
}
