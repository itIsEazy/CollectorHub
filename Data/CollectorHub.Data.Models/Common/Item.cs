namespace CollectorHub.Data.Models.Common
{
    using System.ComponentModel.DataAnnotations;

    using CollectorHub.Data.Common.Models;
    using CollectorHub.Data.Models.Interfaces;

    public abstract class Item : BaseDeletableModel<string>, IItem
    {
        public Item()
        {
        }

        [Range(0, 9999999999999999.99)]
        public decimal PriceNow { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal PriceBoughted { get; set; }

        public string OwnerPictureUrl { get; set; }

        // add bool IsNewCondition (Very Important)
        // add bool UseDefaultPictureForThisItem (Important)
    }
}
