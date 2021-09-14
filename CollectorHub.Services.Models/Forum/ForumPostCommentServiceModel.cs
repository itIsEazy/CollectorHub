namespace CollectorHub.Services.Models.Forum
{
    public class ForumPostCommentServiceModel
    {
        public ForumPostCommentServiceModel()
        {
        }

        public string Id { get; set; }

        public string AuthorId { get; set; }

        public string AuthorUserName { get; set; }

        public string Content { get; set; }

        public int LikesCount { get; set; }

        public string PostId { get; set; }

        public string ParentId { get; set; }
    }
}
