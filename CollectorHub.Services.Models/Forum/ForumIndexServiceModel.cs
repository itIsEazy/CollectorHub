namespace CollectorHub.Services.Models.Forum
{
    using System.Collections.Generic;

    using CollectorHub.Services.Models.Common;

    public class ForumIndexServiceModel
    {
        public ForumIndexServiceModel()
        {
            this.Categories = new HashSet<CategoryServiceModel>();
            this.Sortings = new HashSet<SortingServiceModel>();

            this.TrendingPosts = new HashSet<ForumPostIndexServiceModel>();
            this.PostsByCategory = new HashSet<ForumPostIndexServiceModel>();
        }

        public string CategoryId { get; set; }

        public string CategoryName { get; set; }

        public IEnumerable<CategoryServiceModel> Categories { get; set; }

        public IEnumerable<SortingServiceModel> Sortings { get; set; }

        public ICollection<ForumPostIndexServiceModel> TrendingPosts { get; set; }

        public ICollection<ForumPostIndexServiceModel> PostsByCategory { get; set; }

        public IndexSearchModel SearchModel { get; set; }
    }
}
