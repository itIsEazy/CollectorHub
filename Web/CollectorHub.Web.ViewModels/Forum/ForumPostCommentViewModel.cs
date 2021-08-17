namespace CollectorHub.Web.ViewModels.Forum
{
    using CollectorHub.Data.Models.Forum;
    using CollectorHub.Data.Models.User;

    public class ForumPostCommentViewModel
    {
        public ForumPostCommentViewModel()
        {
        }

        public string Id { get; set; }

        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        public string AuthorUserName { get; set; }

        public string Content { get; set; }

        public int LikesCount { get; set; }

        public string PostId { get; set; }

        public ForumPost Post { get; set; }

        public string ParentId { get; set; }

        public ForumPostComment Parent { get; set; }
    }
}
