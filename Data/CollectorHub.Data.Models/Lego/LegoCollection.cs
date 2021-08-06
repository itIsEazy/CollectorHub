namespace CollectorHub.Data.Models.Lego
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CollectorHub.Data.Common.Models;
    using CollectorHub.Data.Models.User;

    public class LegoCollection : BaseDeletableModel<int>
    {
        public LegoCollection()
        {
            this.Items = new HashSet<LegoCollectionLegoItem>();
        }

        [Required]
        public string Name { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<LegoCollectionLegoItem> Items { get; set; }
    }
}
