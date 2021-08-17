namespace CollectorHub.Data.Models.Lego
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using CollectorHub.Data.Common.Models;

    public class LegoMinifigure : BaseDeletableModel<string>
    {
        //// the link works when u add the SwNumber directly after sw : M=sw0450  (M stands for minifig i guess)
        private string brickLinkCatalogLink = "https://www.bricklink.com/v2/catalog/catalogitem.page?M=sw";
        private string brickLinkPriceGuideLink = "https://www.bricklink.com/catalogPG.asp?M=sw";

        public LegoMinifigure()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string SwNumber { get; set; } // maybe not needed sw0002 => only 0002

        public string Name { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal AvgPriceNew { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal AvgPriceUsed { get; set; }

        public int ProductionYear { get; set; }

        public double WeightInGrams { get; set; } // shows the weight in grams
    }
}
