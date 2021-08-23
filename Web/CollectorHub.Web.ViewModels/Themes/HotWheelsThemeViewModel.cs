namespace CollectorHub.Web.ViewModels.Themes
{
    using System.Collections.Generic;

    using CollectorHub.Data.Models.Collections.HotWheels;

    public class HotWheelsThemeViewModel
    {
        public HotWheelsThemeViewModel()
        {
            this.AllHotWheelsTypes = new HashSet<HotWheelsType>();
            this.AllCars = new HashSet<HotWheelsThemeCarViewModel>();
            this.AllSeries = new HashSet<HotWheelsThemeSerieViewModel>();
        }

        public IEnumerable<HotWheelsType> AllHotWheelsTypes { get; set; }

        public ICollection<HotWheelsThemeCarViewModel> AllCars { get; set; }

        public ICollection<HotWheelsThemeSerieViewModel> AllSeries { get; set; }
    }
}
