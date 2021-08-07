namespace CollectorHub.Data.Models.HotWheels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using CollectorHub.Data.Common.Models;
    using CollectorHub.Data.Models.Common;
    using CollectorHub.Data.Models.User;

    public class FastAndFuriousPremiumCollection : Collection
    {
        public FastAndFuriousPremiumCollection()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Items = new HashSet<FastAndFuriousPremiumItem>();
        }

        public string UserId { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<FastAndFuriousPremiumItem> Items { get; set; }
    }
}
