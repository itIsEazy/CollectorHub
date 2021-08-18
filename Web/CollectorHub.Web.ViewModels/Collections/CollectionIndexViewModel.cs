namespace CollectorHub.Web.ViewModels.Collections
{
    using CollectorHub.Data.Models.User;

    public class CollectionIndexViewModel
    {
        public CollectionIndexViewModel()
        {
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Action { get; set; }

        public int ViewsCount { get; set; }

        public ApplicationUser Owner { get; set; }
    }
}
