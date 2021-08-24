namespace CollectorHub.Web.ViewModels.Collections.Lego
{
    using System.ComponentModel.DataAnnotations;

    public class TransferLegoMinifigureItemInputModel
    {
        public TransferLegoMinifigureItemInputModel()
        {
        }

        [Range(0, 9999999999999999.99)]
        public decimal PriceBoughted { get; set; }

        [Url]
        public string OwnerImageUrl { get; set; }

        public string MinifigureId { get; set; }

        public string ItemId { get; set; }

        public string CollectionId { get; set; }
    }
}
