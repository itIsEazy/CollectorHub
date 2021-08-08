namespace CollectorHub.Data.Models.Interfaces
{
    using System.Collections.Generic;

    using CollectorHub.Data.Models.User;

    public interface IRateStar
    {
        public ICollection<ApplicationUser> Users { get; set; }
    }
}
