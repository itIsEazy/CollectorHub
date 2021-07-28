namespace CollectorHub.Data.Models.HotWheels
{
    using System.Collections.Generic;

    using CollectorHub.Data.Models.Common;

    public class PremiumHWCollection
    {
        public PremiumHWCollection()
        {
            this.Cars = new HashSet<PremiumHWCar>();
        }

        public int Id { get; set; }

        public ApplicationUser User { get; set; }

        public IEnumerable<PremiumHWCar> Cars { get; set; }
    }
}
