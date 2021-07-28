namespace CollectorHub.Data.Models.Common
{
    using System.Collections.Generic;

    using CollectorHub.Data.Common.Models;
    using CollectorHub.Data.Models.Interfaces;

    public abstract class Collection : BaseDeletableModel<int>, ICollection
    {
        public Collection()
        {
        }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
