namespace CollectorHub.Data.Models
{
    using System.Collections.Generic;

    using CollectorHub.Data.Common.Models;

    public class LegoCollection : BaseDeletableModel<int>
    {
        public LegoCollection()
        {
        }

        public List<LegoItem> Items { get; set; }
    }
}
