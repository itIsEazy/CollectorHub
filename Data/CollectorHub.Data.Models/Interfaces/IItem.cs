namespace CollectorHub.Data.Models.Interfaces
{
    using CollectorHub.Data.Common.Models;

    public interface IItem
    {
        public decimal PriceNow { get; set; }

        public decimal PriceBoughted { get; set; }
    }
}
