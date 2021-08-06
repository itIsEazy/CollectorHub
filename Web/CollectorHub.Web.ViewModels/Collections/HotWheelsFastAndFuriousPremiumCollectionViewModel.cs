namespace CollectorHub.Web.ViewModels.Collections
{
    using System.Collections.Generic;

    using CollectorHub.Data.Models.HotWheels;
    using CollectorHub.Data.Models.User;

    public class HotWheelsFastAndFuriousPremiumCollectionViewModel
    {
        public HotWheelsFastAndFuriousPremiumCollectionViewModel()
        {
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public int ViewsCount { get; set; }

        public bool IsPublic { get; set; }

        public string ImageUrl { get; set; }

        public ApplicationUser User { get; set; }

        public IEnumerable<FastAndFuriousPremiumItem> Items { get; set; }
    }
}
