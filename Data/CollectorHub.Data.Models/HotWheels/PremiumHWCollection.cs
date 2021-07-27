namespace CollectorHub.Data.Models.HotWheels
{
    using System.Collections.Generic;

    using CollectorHub.Data.Common.Models;

    public class PremiumHWCollection : BaseDeletableModel<string>
    {
        public PremiumHWCollection()
        {
            this.Cars = new HashSet<PremiumHWCar>();
        }

        public ApplicationUser User { get; set; }

        public IEnumerable<PremiumHWCar> Cars { get; set; }
    }
}
