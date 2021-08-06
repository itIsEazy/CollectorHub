namespace CollectorHub.Data.Models.HotWheels
{
    using System;

    using CollectorHub.Data.Models.Common;

    public class FastAndFuriousPremiumItem : Item
    {
        public FastAndFuriousPremiumItem()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public FastAndFuriousPremiumCar Car { get; set; }
    }
}
