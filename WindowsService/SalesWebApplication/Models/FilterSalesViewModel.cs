using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesWebApplication.Models
{
    public class FilterSalesViewModel
    {
        public Nullable<DateTime> BeginDateTime { get; set; }
        public Nullable<DateTime> EndDateTime { get; set; }
        public string ManagerFiltr { get; set; }
        public string CustomerFiltr { get; set; }
        public string ProductFiltr { get; set; }
    }
}