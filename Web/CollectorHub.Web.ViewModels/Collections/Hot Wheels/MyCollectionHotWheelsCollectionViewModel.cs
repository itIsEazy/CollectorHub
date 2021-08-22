namespace CollectorHub.Web.ViewModels.Collections.Hot_Wheels
{
    public class MyCollectionHotWheelsCollectionViewModel
    {
        public MyCollectionHotWheelsCollectionViewModel()
        {
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public int ViewsCount { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string Progression { get; set; } // shows how much cars u have from the collection Example 12 / 55 Cars Total
    }
}
