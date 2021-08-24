namespace CollectorHub.Web.ViewModels.Collections.Lego
{
    using System.ComponentModel.DataAnnotations;

    public class CreateLegoInputModel
    {
        public CreateLegoInputModel()
        {
        }

        [Required]
        [MinLength(10)]
        [MaxLength(400)]
        public string Description { get; set; }

        public bool IsPublic { get; set; }

        public bool ShowPrices { get; set; }

        public string UserId { get; set; }

        public string LegoTypeId { get; set; }

        public string LegoTypeName { get; set; }
    }
}
