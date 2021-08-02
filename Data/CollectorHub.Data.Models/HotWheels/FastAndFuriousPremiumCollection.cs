﻿namespace CollectorHub.Data.Models.HotWheels
{
    using System;
    using System.Collections.Generic;

    using CollectorHub.Data.Common.Models;
    using CollectorHub.Data.Models.Common;
    using CollectorHub.Data.Models.User;

    public class FastAndFuriousPremiumCollection : Collection
    {
        public FastAndFuriousPremiumCollection()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Cars = new HashSet<FastAndFuriousPremiumCar>();
        }

        public ApplicationUser User { get; set; }

        public IEnumerable<FastAndFuriousPremiumCar> Cars { get; set; }
    }
}
