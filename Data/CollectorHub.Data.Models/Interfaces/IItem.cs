namespace CollectorHub.Data.Models.Interfaces
{
    public interface IItem
    {
        public decimal PriceNow { get; set; }

        public decimal PriceBoughted { get; set; }
    }
}
