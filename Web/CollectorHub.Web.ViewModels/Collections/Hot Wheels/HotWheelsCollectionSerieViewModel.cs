namespace CollectorHub.Web.ViewModels.Collections.Hot_Wheels
{
    using System.Collections.Generic;

    public class HotWheelsCollectionSerieViewModel
    {
        public HotWheelsCollectionSerieViewModel()
        {
            this.Cars = new HashSet<HotWheelsCollectionCarViewModel>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Year { get; set; }

        public ICollection<HotWheelsCollectionCarViewModel> Cars { get; set; }
    }
}
