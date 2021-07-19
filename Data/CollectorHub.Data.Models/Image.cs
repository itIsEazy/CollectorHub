namespace CollectorHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using CollectorHub.Data.Common.Models;

    public class Image : BaseDeletableModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
