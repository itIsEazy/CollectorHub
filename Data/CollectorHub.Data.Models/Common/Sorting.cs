namespace CollectorHub.Data.Models.Common
{
    using CollectorHub.Data.Common.Models;

    public class Sorting : BaseDeletableModel<int>
    {
        public Sorting()
        {
        }

        public string Name { get; set; }
    }
}
