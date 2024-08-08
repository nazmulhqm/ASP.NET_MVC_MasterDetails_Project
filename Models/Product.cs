using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCProject_Nazmul.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Name is Required!")]
        [StringLength(100, ErrorMessage = "Name Must Be in 100 Character")]
        public string ProductDescription { get; set; }

        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}