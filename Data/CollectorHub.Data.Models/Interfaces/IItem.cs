namespace CollectorHub.Data.Models.Interfaces
{
    public interface IItem
    {
        public decimal PriceNow { get; set; }

        public decimal PriceBoughted { get; set; }

        public string OwnerPictureUrl { get; set; }

        public bool ConditionIsNew { get; set; }

        public decimal Profit { get; set; }
    }
}
