namespace CollectorHub.Web.ViewModels.Collections.Hot_Wheels
{
    using System.Collections.Generic;

    public class HotWheelsCollectionViewModel
    {
        public HotWheelsCollectionViewModel()
        {
            this.Items = new HashSet<HotWheelsCollectionCarItemViewModel>();
            this.AllSeries = new HashSet<HotWheelsCollectionSerieViewModel>();
        }

        public string Id { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int ViewsCount { get; set; }

        public bool IsPublic { get; set; }

        public bool ShowPrices { get; set; }

        public string ImageUrl { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string TypeId { get; set; }

        public string TypeName { get; set; }

        public string CollectionTypeId { get; set; }

        public string CollectionTypeName { get; set; }

        public IEnumerable<HotWheelsCollectionCarItemViewModel> Items { get; set; }

        public IEnumerable<HotWheelsCollectionSerieViewModel> AllSeries { get; set; }

        public TransferHotWheelsCarItemInputModel SelectedModel { get; set; }
    }
}
