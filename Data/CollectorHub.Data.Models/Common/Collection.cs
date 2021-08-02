namespace CollectorHub.Data.Models.Common
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CollectorHub.Data.Common.Models;
    using CollectorHub.Data.Models.Interfaces;

    public abstract class Collection : BaseDeletableModel<string>, ICollection
    {
        public Collection()
        {
        }

        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(400)]
        public string Description { get; set; }

        [Url]
        public string ImageUrl { get; set; }
    }
}
