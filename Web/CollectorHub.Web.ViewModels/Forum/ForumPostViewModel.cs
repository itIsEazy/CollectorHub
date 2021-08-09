﻿namespace CollectorHub.Web.ViewModels.Forum
{
    using System.Collections.Generic;

    using CollectorHub.Data.Models.Common;
    using CollectorHub.Data.Models.Forum;
    using CollectorHub.Data.Models.User;

    public class ForumPostViewModel
    {
        public ForumPostViewModel()
        {
        }

        public string Id { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public virtual Category Category { get; set; }

        public int ViewsCount { get; set; }

        public int LikesCount { get; set; }

        public int StarsCount { get; set; }

        public ICollection<ForumPostComment> Comments { get; set; }

        public ICollection<ForumStar> Stars { get; set; }
    }
}