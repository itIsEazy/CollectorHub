namespace CollectorHub.Data.Models.Collections.HotWheels
{
    using System;
    using System.Collections.Generic;

    using CollectorHub.Data.Models.Common;

    public class FastAndFuriousPremiumCollection : Collection
    {
        public FastAndFuriousPremiumCollection()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Items = new HashSet<FastAndFuriousPremiumItem>();
        }

        public virtual ICollection<FastAndFuriousPremiumItem> Items { get; set; }
    }
}
