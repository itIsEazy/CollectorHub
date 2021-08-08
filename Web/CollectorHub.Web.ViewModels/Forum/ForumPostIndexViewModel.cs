namespace CollectorHub.Web.ViewModels.Forum
{
    using CollectorHub.Data.Models.Common;

    public class ForumPostIndexViewModel
    {
        public ForumPostIndexViewModel()
        {
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public Category Category { get; set; }

        public string ImageUrl { get; set; }

        public int StarsCount { get; set; }

        public int CommentCount { get; set; }

        public int ViewCount { get; set; }

        public string Date { get; set; }

    }
}
