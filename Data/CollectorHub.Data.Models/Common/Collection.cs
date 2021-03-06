namespace CollectorHub.Data.Models.Common
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CollectorHub.Data.Common.Models;
    using CollectorHub.Data.Models.Collections;
    using CollectorHub.Data.Models.Interfaces;
    using CollectorHub.Data.Models.User;

    public abstract class Collection : BaseDeletableModel<string>, ICollection
    {
        public Collection()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(400)]
        public string Description { get; set; }

        public int ViewsCount { get; set; }

        public bool IsPublic { get; set; }

        public bool ShowPrices { get; set; }

        [Url]
        public string ImageUrl { get; set; }

        public string CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string CollectionTypeId { get; set; }

        public virtual CollectionType CollectionType { get; set; }

        // add bool ShowCollectionPrice / ShowItemsPrice (Very Important)
    }
}
