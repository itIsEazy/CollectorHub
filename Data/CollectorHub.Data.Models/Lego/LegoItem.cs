namespace CollectorHub.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CollectorHub.Data.Common.Models;
    using CollectorHub.Data.Models.Lego;

    public class LegoItem : BaseDeletableModel<int>
    {
        private string brickLinkCatalogLink = "https://www.bricklink.com/v2/catalog/catalogitem.page?M=sw";
        private string brickLinkPriceGuideLink = "https://www.bricklink.com/catalogPG.asp?M=sw";
        private string conditionNEW = "NEW";
        private string conditionUSED = "USED";

        public LegoItem()
        {
            this.Collections = new HashSet<LegoCollectionLegoItem>();
        }

        public string SwNumber { get; set; } // maybe not needed sw0002 => only 0002

        public string CatalogLink { get; set; } // https://www.bricklink.com/v2/catalog/catalogitem.page?M=sw0454

        public string PriceGuideLink { get; set; } // https://www.bricklink.com/catalogPG.asp?M=sw0454

        public string Name { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal PriceNow { get; set; } // IN LEVA

        [Range(0, 9999999999999999.99)]
        public decimal AvgPriceNew { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal AvgPriceUsed { get; set; }

        public int ProductionYear { get; set; }

        public double WeightInGrams { get; set; } // shows the weight in grams

        public string Condition { get; set; } // NEW / USED

        [Range(0, 9999999999999999.99)]
        public decimal PriceBoughted { get; set; }

        [Range(0, 9999999999999999.99)]
        public decimal Profit { get; set; }

        public int CollectionId { get; set; }

        public virtual ICollection<LegoCollectionLegoItem> Collections { get; set; }
    }
}
