namespace CollectorHub.Web.ViewModels.Collections.Hot_Wheels
{
    using System.Collections.Generic;

    using CollectorHub.Web.ViewModels.Collections.Lego;

    public class MyCollectionIndexViewModel
    {
        public MyCollectionIndexViewModel()
        {
            this.HotWheelsCollections = new HashSet<MyCollectionHotWheelsCollectionViewModel>();
            this.LegoCollections = new HashSet<MyCollectionLegoCollectionViewModel>();
        }

        public IEnumerable<MyCollectionHotWheelsCollectionViewModel> HotWheelsCollections { get; set; }

        public IEnumerable<MyCollectionLegoCollectionViewModel> LegoCollections { get; set; }
    }
}
