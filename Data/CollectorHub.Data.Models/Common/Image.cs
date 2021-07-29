namespace CollectorHub.Data.Models.Common
{
    using System;

    using CollectorHub.Data.Common.Models;

    public class Image : BaseDeletableModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
