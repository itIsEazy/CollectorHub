namespace CollectorHub.Web.ViewModels.Collections.Common
{
    public class TrendingCollectionViewModel
    {
        public TrendingCollectionViewModel()
        {
        }

        public string Id { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Description { get; set; }

        public int ViewsCount { get; set; }

        public bool IsPublic { get; set; }

        public bool ShowPrices { get; set; }

        public string ImageUrl { get; set; }

        public string CategoryId { get; set; }

        public string Action { get; set; }

        public string DateCreated { get; set; }
    }
}
