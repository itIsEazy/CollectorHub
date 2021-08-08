namespace CollectorHub.Data.Models.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CollectorHub.Data.Common.Models;
    using CollectorHub.Data.Models.Forum;
    using CollectorHub.Data.Models.HotWheels;

    public class Category : BaseModel<string>
    {
        public Category()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        public virtual ICollection<ForumPost> Posts { get; set; }

        public virtual ICollection<FastAndFuriousPremiumCollection> FastAndFuriousPremiumCollection { get; set; }

        // DO NOT FORGET to add ICollectio<> every time when u connect Category to some Entity
        // MAYBE REMOVE ALL THIS COLLECTION
    }
}
