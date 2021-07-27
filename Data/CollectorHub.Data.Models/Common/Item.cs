namespace CollectorHub.Data.Models.Common
{
    public abstract class Item : IItem
    {
        public Item()
        {
        }

        public decimal PriceNow { get; set; }

        public decimal PriceBoughted { get; set; }
    }
}
