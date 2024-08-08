using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject_Nazmul.Models.ViewModels
{
    public class OrderViewModel
    {
        //Customer
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Gender { get; set; }
        public string Addresses { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        //OrderDetails
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Order
        public DateTime OrderDate { get; set; }
        public string Note { get; set; }
        public string Image { get; set; }

        // ......
        public bool Terms { get; set; }
    }
}