using System;

namespace SalesWebApplication.Models
{
    public class SalesViewModel
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime DateTime { get; set; }
        public string ManagerName { get; set; }
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
    }
}