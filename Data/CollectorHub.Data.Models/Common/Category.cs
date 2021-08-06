namespace CollectorHub.Data.Models.Common
{
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public Category()
        {
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public virtual SubCategory SubCategory { get; set; }
    }
}
