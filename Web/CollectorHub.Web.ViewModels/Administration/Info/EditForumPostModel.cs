namespace CollectorHub.Web.ViewModels.Administration.Info
{
    using System.Collections.Generic;

    using CollectorHub.Data.Models.Common;
    using CollectorHub.Data.Models.Forum;
    using CollectorHub.Web.ViewModels.Common;

    public class EditForumPostModel
    {
        public EditForumPostModel()
        {
            this.Stars = new HashSet<ForumStar>();
            this.Categories = new HashSet<CategoryIndexViewModel>();
        }

        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Needs to have atributes
        /// </summary>

        public string Id { get; set; }

        public string AuthorId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public bool IsVerified { get; set; }

        public int ViewsCount { get; set; }

        public int LikesCount { get; set; }

        public int StarsCount { get; set; }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public virtual Category Category { get; set; }

        public virtual IEnumerable<ForumStar> Stars { get; set; }

        public IEnumerable<CategoryIndexViewModel> Categories { get; set; }
    }
}
