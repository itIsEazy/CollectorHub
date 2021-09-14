using System.Collections.Generic;

namespace CollectorHub.Services.Models.Forum
{
    public class ForumPostIndexServiceModel
    {
        public ForumPostIndexServiceModel()
        {
            this.Comments = new HashSet<ForumPostCommentServiceModel>();
        }

        public string Id { get; set; }

        public string OwnerUserName { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string CategoryName { get; set; }

        public string ImageUrl { get; set; }

        public int StarsCount { get; set; }

        public int CommentCount { get; set; }

        public int ViewCount { get; set; }

        public string Date { get; set; }

        public IEnumerable<ForumPostCommentServiceModel> Comments { get; set; }
    }
}
