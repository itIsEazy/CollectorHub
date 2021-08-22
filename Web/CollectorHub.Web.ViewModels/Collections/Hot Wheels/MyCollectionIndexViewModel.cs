namespace CollectorHub.Web.ViewModels.Collections.Hot_Wheels
{
    using System.Collections.Generic;

    public class MyCollectionIndexViewModel
    {
        public MyCollectionIndexViewModel()
        {
            this.HotWheelsCollections = new HashSet<MyCollectionHotWheelsCollectionViewModel>();
        }

        public IEnumerable<MyCollectionHotWheelsCollectionViewModel> HotWheelsCollections { get; set; }
    }
}
