namespace CollectorHub.Web.ViewModels.Themes
{
    using System.Collections.Generic;

    using CollectorHub.Data.Models.Collections.HotWheels;

    public class HotWheelsPremiumSeriesViewModel
    {
        public HotWheelsPremiumSeriesViewModel()
        {
            this.Cars = new HashSet<FastAndFuriousPremiumCar>();
        }

        public string Id { get; set; }

        public string Year { get; set; }

        public string Name { get; set; }

        public int OrderOfAppearence { get; set; }

        public ICollection<FastAndFuriousPremiumCar> Cars { get; set; }
    }
}
