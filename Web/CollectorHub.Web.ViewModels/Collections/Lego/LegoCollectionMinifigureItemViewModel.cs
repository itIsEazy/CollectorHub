namespace CollectorHub.Web.ViewModels.Collections.Lego
{
    public class LegoCollectionMinifigureItemViewModel
    {
        public LegoCollectionMinifigureItemViewModel()
        {
        }

        public string Id { get; set; }

        public decimal PriceBoughted { get; set; }

        public string ImageUrl { get; set; }

        public virtual LegoCollectionMinifigureViewModel Minifigure { get; set; }
    }
}
