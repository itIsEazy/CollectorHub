namespace CollectorHub.Data.Models.Common
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CollectorHub.Data.Common.Models;

    public abstract class BaseType : BaseDeletableModel<string>
    {
        public BaseType()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Name { get; set; }
    }
}
