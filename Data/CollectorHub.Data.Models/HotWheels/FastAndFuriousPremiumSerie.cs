namespace CollectorHub.Data.Models.HotWheels
{
    using System;
    using System.Collections.Generic;

    using CollectorHub.Data.Common.Models;

    public class FastAndFuriousPremiumSerie : BaseDeletableModel<string>
    {
        public FastAndFuriousPremiumSerie()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Cars = new HashSet<FastAndFuriousPremiumCar>();
        }

        public string Year { get; set; }

        public string Name { get; set; }

        public int OrderOfApperance { get; set; }

        public ICollection<FastAndFuriousPremiumCar> Cars { get; set; }
    }
}
