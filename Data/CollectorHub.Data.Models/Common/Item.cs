namespace CollectorHub.Data.Models.Common
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using CollectorHub.Data.Common.Models;
    using CollectorHub.Data.Models.Interfaces;

    public abstract class Item : BaseDeletableModel<string>, IItem
    {
        public Item()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal PriceNow { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal PriceBoughted { get; set; }

        public string OwnerPictureUrl { get; set; }

        public bool ConditionIsNew { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Profit { get; set; }

        // add bool UseDefaultPictureForThisItem (Important)
    }
}
