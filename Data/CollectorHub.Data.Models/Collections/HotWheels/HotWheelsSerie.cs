namespace CollectorHub.Data.Models.Collections.HotWheels
{
    using System;
    using System.Collections.Generic;

    using CollectorHub.Data.Common.Models;

    public class HotWheelsSerie : BaseDeletableModel<string>
    {
        public HotWheelsSerie()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Cars = new HashSet<HotWheelsCar>();
        }

        public string Year { get; set; }

        public string Name { get; set; }

        public int OrderOfApperance { get; set; }

        public string HotWheelsTypeId { get; set; }

        public virtual HotWheelsType HotWheelsType { get; set; }

        public virtual ICollection<HotWheelsCar> Cars { get; set; }
    }
}
