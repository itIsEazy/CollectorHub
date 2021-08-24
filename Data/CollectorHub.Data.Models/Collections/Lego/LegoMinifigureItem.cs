namespace CollectorHub.Data.Models.Collections.Lego
{
    using System.Collections.Generic;

    using CollectorHub.Data.Models.Common;

    public class LegoMinifigureItem : Item
    {
        public LegoMinifigureItem()
        {
        }

        public string MinifigureId { get; set; }

        public virtual LegoMinifigure Minifigure { get; set; }

        public string CollectionId { get; set; }

        public virtual LegoCollection Collection { get; set; }
    }
}
