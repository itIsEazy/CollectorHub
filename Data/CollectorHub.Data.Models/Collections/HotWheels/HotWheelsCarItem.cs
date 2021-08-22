namespace CollectorHub.Data.Models.Collections.HotWheels
{
    using System.Collections.Generic;

    using CollectorHub.Data.Models.Common;

    public class HotWheelsCarItem : Item
    {
        public HotWheelsCarItem()
        {
        }

        public string CarId { get; set; }

        public virtual HotWheelsCar Car { get; set; }

        public string CollectionId { get; set; }

        public virtual HotWheelsCollection Collection { get; set; }
    }
}
