namespace CollectorHub.Web.ViewModels.Collections
{
    using System.ComponentModel.DataAnnotations;

    using CollectorHub.Data.Models.User;

    public class CreateHotWheelsFastAndFuriousPremiumCollectionInputModel
    {
        public CreateHotWheelsFastAndFuriousPremiumCollectionInputModel()
        {
        }

        [Required]
        [MinLength(10)]
        [MaxLength(400)]
        public string Description { get; set; }

        public bool IsPublic { get; set; }

        public bool ShowPrices { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
