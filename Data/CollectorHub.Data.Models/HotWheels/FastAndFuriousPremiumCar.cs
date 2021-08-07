namespace CollectorHub.Data.Models.HotWheels
{
    using System;
    using System.Collections.Generic;

    using CollectorHub.Data.Common.Models;
    using CollectorHub.Data.Models.Common;

    public class FastAndFuriousPremiumCar : BaseDeletableModel<string>
    {
        public FastAndFuriousPremiumCar()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Items = new HashSet<FastAndFuriousPremiumItem>();
        }

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

        public string SerieId { get; set; }

        public virtual FastAndFuriousPremiumSerie Serie { get; set; }

        public virtual ICollection<FastAndFuriousPremiumItem> Items { get; set; }
    }
}
