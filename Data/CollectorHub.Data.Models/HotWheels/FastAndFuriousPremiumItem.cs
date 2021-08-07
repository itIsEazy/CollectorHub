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

        public string CarId { get; set; }

        public virtual FastAndFuriousPremiumCar Car { get; set; }

        public string CollectionId { get; set; }

        public virtual FastAndFuriousPremiumCollection Collection { get; set; }
    }
}
