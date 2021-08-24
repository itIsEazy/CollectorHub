namespace CollectorHub.Web.ViewModels.Collections
{
    using System.Collections.Generic;

    using CollectorHub.Data.Models.Collections;
    using CollectorHub.Data.Models.Collections.HotWheels;
    using CollectorHub.Data.Models.Collections.Lego;

    public class CreateIndexViewModel
    {
        public CreateIndexViewModel()
        {
            this.AllCollectionTypes = new HashSet<CollectionType>();
            this.AllHotWheelsTypes = new HashSet<HotWheelsType>();
            this.AllLegoTypes = new HashSet<LegoType>();
        }

        public IEnumerable<CollectionType> AllCollectionTypes { get; set; }

        public IEnumerable<HotWheelsType> AllHotWheelsTypes { get; set; }

        public IEnumerable<LegoType> AllLegoTypes { get; set; }
    }
}
