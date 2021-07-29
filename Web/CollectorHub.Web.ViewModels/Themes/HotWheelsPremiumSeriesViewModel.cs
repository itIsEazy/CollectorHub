namespace CollectorHub.Web.ViewModels.Themes
{
    using System.Collections.Generic;

    using CollectorHub.Data.Models.HotWheels;

    public class HotWheelsPremiumSeriesViewModel
    {
        public HotWheelsPremiumSeriesViewModel()
        {
            this.Cars = new HashSet<PremiumHWCar>();
        }

        public int Id { get; set; }

        public string Year { get; set; }

        public string Name { get; set; }

        public ICollection<PremiumHWCar> Cars { get; set; }
    }
}
