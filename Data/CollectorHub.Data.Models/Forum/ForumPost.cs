namespace CollectorHub.Data.Models.Forum
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CollectorHub.Data.Common.Models;
    using CollectorHub.Data.Models.Common;
    using CollectorHub.Data.Models.User;

    public class ForumPost : BaseDeletableModel<string>
    {
        public ForumPost()
        {
            this.Id = Guid.NewGuid().ToString();

            this.ViewsCount = 0;
            this.LikesCount = 0;
            this.StarsCount = 0;

            this.Comments = new HashSet<ForumPostComment>();
        }

        [Required]
        public string AuthorId { get; set; }

        [Required]
        public virtual ApplicationUser Author { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        public string Content { get; set; }

        [Url]
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public int ViewsCount { get; set; }

        public int LikesCount { get; set; }

        public int StarsCount { get; set; }

        public virtual ICollection<ForumPostComment> Comments { get; set; }
    }
}
