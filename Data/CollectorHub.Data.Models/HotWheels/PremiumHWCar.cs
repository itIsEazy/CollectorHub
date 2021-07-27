namespace CollectorHub.Data.Models.HotWheels
{
    using System.Collections.Generic;

    using CollectorHub.Data.Models.Common;

    public class PremiumHWCar : Item
    {
        public PremiumHWCar()
        {
            this.Collections = new HashSet<PremiumHWCollection>();
        }

        public int Id { get; set; }

        public string Col { get; set; }

        public string ToyId { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }

        public string Tampos { get; set; }

        public string WheelType { get; set; }

        public string Movie { get; set; }

        public string Notes { get; set; }

        public string PhotoLooseLink { get; set; }

        public string PhotoCardLink { get; set; }

        public int SerieId { get; set; }

        public PremiumHWSerie Serie { get; set; }

        public IEnumerable<PremiumHWCollection> Collections { get; set; }
    }
}
