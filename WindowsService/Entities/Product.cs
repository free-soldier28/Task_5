using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public partial class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public virtual ICollection<Sales> Sales { get; set; }
    }
}
