namespace CollectorHub.Web.ViewModels.Collections
{
    using System.Collections.Generic;

    using CollectorHub.Data.Models.Collections;
    using CollectorHub.Data.Models.Collections.HotWheels;

    public class CreateIndexViewModel
    {
        public CreateIndexViewModel()
        {
            this.AllCollectionTypes = new HashSet<CollectionType>();
            this.AllHotWheelsTypes = new HashSet<HotWheelsType>();
        }

        public IEnumerable<CollectionType> AllCollectionTypes { get; set; }

        public IEnumerable<HotWheelsType> AllHotWheelsTypes { get; set; }
    }
}
