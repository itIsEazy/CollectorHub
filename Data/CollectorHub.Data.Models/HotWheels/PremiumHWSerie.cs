namespace CollectorHub.Data.Models.HotWheels
{
    using System.Collections.Generic;

    public class PremiumHWSerie
    {
        public PremiumHWSerie()
        {
            this.Cars = new HashSet<PremiumHWCar>();
        }

        public int Id { get; set; }

        public string Year { get; set; }

        public string Name { get; set; }

        public int OrderOfApperance { get; set; }

        public ICollection<PremiumHWCar> Cars { get; set; }

    }
}
