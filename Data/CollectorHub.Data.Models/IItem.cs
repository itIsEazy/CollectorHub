namespace CollectorHub.Data.Models
{
    using CollectorHub.Data.Common.Models;

    public interface IItem
    {
        public decimal PriceBoughted { get; set; }

        public decimal Profit { get; set; }

        public int CollectionId { get; set; }

        public abstract ICollection Collection { get; set; }
    }
}
