namespace CollectorHub.Data.Models.Collections.HotWheels
{
    using System.Collections.Generic;

    using CollectorHub.Data.Models.Common;

    public class HotWheelsCollection : Collection
    {
        public HotWheelsCollection()
        {
            this.Items = new HashSet<HotWheelsCarItem>();
        }

        public string HotWheelsTypeId { get; set; }

        public virtual HotWheelsType HotWheelsType { get; set; }

        public virtual IEnumerable<HotWheelsCarItem> Items { get; set; }
    }
}
