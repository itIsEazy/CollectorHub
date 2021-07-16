namespace CollectorHub.Data.Models
{
    using CollectorHub.Data.Common.Models;

    public class LegoItem : BaseDeletableModel<int>
    {
        private string brickLinkCatalogLink = "https://www.bricklink.com/v2/catalog/catalogitem.page?M=sw";
        private string brickLinkPriceGuideLink = "https://www.bricklink.com/catalogPG.asp?M=sw";
        private string conditionNEW = "NEW";
        private string conditionUSED = "USED";

        public LegoItem()
        {
        }

        public string SwNumber { get; set; } // maybe not needed sw0002 => only 0002

        public string CatalogLink { get; set; } // https://www.bricklink.com/v2/catalog/catalogitem.page?M=sw0454

        public string PriceGuideLink { get; set; } // https://www.bricklink.com/catalogPG.asp?M=sw0454

        public string Name { get; set; }

        public decimal PriceNow { get; set; } // IN LEVA

        public decimal AvgPriceNew { get; set; }

        public decimal AvgPriceUsed { get; set; }

        public int ProductionYear { get; set; }

        public double WeightInGrams { get; set; } // shows the weight in grams

        public string Condition { get; set; } // NEW / USED

        public decimal PriceBoughted { get; set; }

        public decimal Profit { get; set; }

        public int CollectionId { get; set; }

        public LegoCollection Collection { get; set; }
    }
}
