namespace CollectorHub.Data.Models.Collections.Lego
{
    using System.Collections.Generic;

    using CollectorHub.Data.Models.Common;

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
