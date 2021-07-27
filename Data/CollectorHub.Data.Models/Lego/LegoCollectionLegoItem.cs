namespace CollectorHub.Data.Models.Lego
{
    public class LegoCollectionLegoItem
    {
        public int Id { get; set; }

        public int CollectionId { get; set; }

        public virtual LegoCollection Collection { get; set; }

        public int ItemId { get; set; }

        public virtual LegoItem Item { get; set; }
    }
}
