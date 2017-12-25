using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public partial class Customer
    { 
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string FullName { get; set; }

        public virtual ICollection<Sales> Sales { get; set; }
    }
}
