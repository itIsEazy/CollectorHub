namespace CollectorHub.Web.ViewModels.Collections.Hot_Wheels
{
    using System.Collections.Generic;

    public class HotWheelsCollectionViewModel
    {
        public HotWheelsCollectionViewModel()
        {
            //this.Items = new HashSet<HotWheelsCarItem>();
        }

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

        //public virtual IEnumerable<HotWheelsCarItem> Items { get; set; }
    }
}
