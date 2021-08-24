namespace CollectorHub.Data.Models.Collections.Lego
{
    using System;
    using System.Collections.Generic;

    using CollectorHub.Data.Models.Common;

    public class LegoCollection : Collection
    {
        public LegoCollection()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Items = new HashSet<LegoMinifigureItem>();
        }

        public string LegoTypeId { get; set; }

        public virtual LegoType LegoType { get; set; }

        public virtual ICollection<LegoMinifigureItem> Items { get; set; }
    }
}
