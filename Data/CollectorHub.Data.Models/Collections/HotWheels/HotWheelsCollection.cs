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

        public string TypeId { get; set; }

        public virtual HotWheelsType Type { get; set; }

        public virtual IEnumerable<HotWheelsCarItem> Items { get; set; }
    }
}
