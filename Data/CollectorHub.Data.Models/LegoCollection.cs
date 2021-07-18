namespace CollectorHub.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CollectorHub.Data.Common.Models;

    public class LegoCollection : BaseDeletableModel<int>
    {
        public LegoCollection()
        {
            this.Items = new HashSet<LegoCollectionLegoItem>();
        }

        [Required]
        public string Name { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<LegoCollectionLegoItem> Items { get; set; }
    }
}
