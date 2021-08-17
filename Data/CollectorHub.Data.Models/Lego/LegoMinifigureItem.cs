namespace CollectorHub.Data.Models
{
    using System.Collections.Generic;

    using CollectorHub.Data.Models.Common;
    using CollectorHub.Data.Models.Lego;

    public class LegoMinifigureItem : Item
    {
        public LegoMinifigureItem()
        {
            this.Collections = new HashSet<LegoCollection>();
        }

        public string MinifigureId { get; set; }

        public virtual LegoMinifigure Minifigure { get; set; }

        public virtual ICollection<LegoCollection> Collections { get; set; }
    }
}
