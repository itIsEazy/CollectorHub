namespace CollectorHub.Data.Models.Common
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CollectorHub.Data.Common.Models;

    public class SubCategory : BaseDeletableModel<string>
    {
        public SubCategory()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public virtual SubCategory Subcategory { get; set; }
    }
}
