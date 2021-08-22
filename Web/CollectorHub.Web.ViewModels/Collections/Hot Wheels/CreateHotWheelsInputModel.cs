namespace CollectorHub.Web.ViewModels.Collections.Hot_Wheels
{
    using System.ComponentModel.DataAnnotations;

    public class CreateHotWheelsInputModel
    {
        public CreateHotWheelsInputModel()
        {
        }

        [Required]
        [MinLength(10)]
        [MaxLength(400)]
        public string Description { get; set; }

        public bool IsPublic { get; set; }

        public bool ShowPrices { get; set; }

        public string UserId { get; set; }

        public string HotWheelsTypeId { get; set; }

        public string HotWheelsTypeName { get; set; }
    }
}
