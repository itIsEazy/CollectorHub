namespace CollectorHub.Web.ViewModels.Collections
{
    using System.ComponentModel.DataAnnotations;

    public class AddHotWheelsFastAndFuriousPremiumItemInputModel
    {
        public AddHotWheelsFastAndFuriousPremiumItemInputModel()
        {
        }

        [Range(0, 9999999999999999.99)]
        public decimal PriceBoughted { get; set; }

        [Url]
        public string OwnerPictureUrl { get; set; }

        public string CarId { get; set; }

        public string CollectionId { get; set; }

        // nice to have : add currentUserId and in service check if collection.User.Id == currentUserId
    }
}
