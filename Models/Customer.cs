using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MVCProject_Nazmul.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [StringLength(50)]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        public string Gender { get; set; }

        public string Addresses { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Image { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}