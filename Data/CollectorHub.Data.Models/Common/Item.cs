namespace CollectorHub.Data.Models.Common
{
    using CollectorHub.Data.Common.Models;
    using CollectorHub.Data.Models.Interfaces;

    public abstract class Item : BaseDeletableModel<int>, IItem
    {
        public Item()
        {
        }

        public decimal PriceNow { get; set; }

        public decimal PriceBoughted { get; set; }
    }
}
