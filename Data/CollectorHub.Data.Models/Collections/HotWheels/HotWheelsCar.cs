namespace CollectorHub.Data.Models.Collections.HotWheels
{
    using System;
    using System.Collections.Generic;

    using CollectorHub.Data.Common.Models;
    using CollectorHub.Data.Models.Collections.HotWheels.Interfaces;

    public class HotWheelsCar : BaseDeletableModel<string>, ICar
    {
        public HotWheelsCar()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Items = new HashSet<HotWheelsCarItem>();
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

        public virtual HotWheelsSerie Serie { get; set; }

        public string TypeId { get; set; }

        public virtual HotWheelsType Type { get; set; }

        public virtual IEnumerable<HotWheelsCarItem> Items { get; set; }
    }
}
