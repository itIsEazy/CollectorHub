namespace CollectorHub.Web.ViewModels.Collections.Lego
{
    using System.Collections.Generic;

    public class LegoCollectionViewModel
    {
        public LegoCollectionViewModel()
        {
            this.Items = new HashSet<LegoCollectionMinifigureItemViewModel>();
            this.AllMinifigures = new HashSet<LegoCollectionMinifigureViewModel>();
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

        public IEnumerable<LegoCollectionMinifigureItemViewModel> Items { get; set; }

        public TransferLegoMinifigureItemInputModel SelectedModel { get; set; }

        public IEnumerable<LegoCollectionMinifigureViewModel> AllMinifigures { get; set; }
    }
}
