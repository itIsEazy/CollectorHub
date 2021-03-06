// ReSharper disable VirtualMemberCallInConstructor
namespace CollectorHub.Data.Models.User
{
    using System;
    using System.Collections.Generic;

    using CollectorHub.Data.Common.Models;
    using CollectorHub.Data.Models.Collections.HotWheels;
    using CollectorHub.Data.Models.Collections.Lego;
    using CollectorHub.Data.Models.Forum;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();

            this.CreatedOn = DateTime.UtcNow;

            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();

            this.HotWheelsCollections = new HashSet<HotWheelsCollection>();
            this.LegoCollections = new HashSet<LegoCollection>();
            this.ForumPosts = new HashSet<ForumPost>();
        }

        public virtual IEnumerable<HotWheelsCollection> HotWheelsCollections { get; set; }

        public virtual IEnumerable<LegoCollection> LegoCollections { get; set; }

        public virtual ICollection<ForumPost> ForumPosts { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
