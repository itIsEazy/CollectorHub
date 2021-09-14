namespace CollectorHub.Services.Models.Forum
{
    using System.Collections.Generic;

    public class ForumPostServiceModel
    {
        public ForumPostServiceModel()
        {
        }

        public string Id { get; set; }

        public string AuthorId { get; set; }

        public string AuthorUserName { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public bool IsVerified { get; set; }

        public string CategoryName { get; set; }

        public string DateCreated { get; set; }

        public int ViewsCount { get; set; }

        public int LikesCount { get; set; }

        public int StarsCount { get; set; }

        public IEnumerable<ForumPostCommentServiceModel> Comments { get; set; }

        public ForumPostCommentInputModel CommentInput { get; set; }
    }
}
