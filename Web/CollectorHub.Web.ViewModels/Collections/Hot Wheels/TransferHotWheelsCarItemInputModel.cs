namespace CollectorHub.Web.ViewModels.Collections.Hot_Wheels
{
    using System.ComponentModel.DataAnnotations;

    public class TransferHotWheelsCarItemInputModel
    {
        public TransferHotWheelsCarItemInputModel()
        {
        }

        [Range(0, 9999999999999999.99)]
        public decimal PriceBoughted { get; set; }

        [Url]
        public string OwnerImageUrl { get; set; }

        public string CarId { get; set; }

        public string ItemId { get; set; }

        public string CollectionId { get; set; }
    }
}
