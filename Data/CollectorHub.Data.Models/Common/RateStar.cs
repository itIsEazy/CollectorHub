namespace CollectorHub.Data.Models.Common
{
    using System;
    using System.Collections.Generic;

    using CollectorHub.Data.Common.Models;
    using CollectorHub.Data.Models.Interfaces;
    using CollectorHub.Data.Models.User;

    public abstract class RateStar : BaseDeletableModel<string>, IRateStar
    {
        public RateStar()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Users = new HashSet<ApplicationUser>();
        }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
