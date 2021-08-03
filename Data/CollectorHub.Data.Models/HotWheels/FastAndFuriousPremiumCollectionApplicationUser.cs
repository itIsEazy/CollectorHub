namespace CollectorHub.Data.Models.HotWheels
{
    using System;
    using System.Collections.Generic;

    using CollectorHub.Data.Common.Models;
    using CollectorHub.Data.Models.User;

    public class FastAndFuriousPremiumCollectionApplicationUser
    {
        public FastAndFuriousPremiumCollectionApplicationUser()
        {
        }

        public int Id { get; set; }

        public string CollectionId { get; set; }

        public ICollection<FastAndFuriousPremiumCollection> Collections { get; set; }

        public string UserId { get; set; }

        public ICollection<ApplicationUser> User { get; set; }
    }
}
