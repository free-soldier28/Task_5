using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public partial class Sales
    {
        public int Id { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }


        public int ManagerID { get; set; }

        public int CustomerID { get; set; }

        public int ProductID { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Manager Manager { get; set; }

        public virtual Product Product { get; set; }
    }
}
