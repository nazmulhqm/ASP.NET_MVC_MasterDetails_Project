using MVCProject_Nazmul.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace MVCProject_Nazmul.DAL
{
    public class DataSeeder
    {
        public void Seed(ApplicationDbContext context)
        {
            context.Categories.AddOrUpdate(
                c=>c.CategoryName,
                new Category { CategoryName= "Laptops", CategoryDescription= "Portable computers and accessories" },
                new Category { CategoryName= "Mobile Phones", CategoryDescription= "Cell phones and accessories" },
                new Category { CategoryName= "Electronics", CategoryDescription= "Electronic devices and accessories" }
                );
            context.Products.AddOrUpdate(
                p=>p.ProductName,
                new Product { ProductName = "Apple MacBook Pro 13-inch",ProductDescription= "Thin and light laptop with Retina display", CategoryId=1 },
                new Product { ProductName = "iPhone 13 Pro Max", ProductDescription= "Apple smartphone with Pro camera system", CategoryId=2 },
                new Product { ProductName = "Microsoft Surface Pro 8", ProductDescription= "Versatile 2-in-1 laptop and tablet with Windows 11", CategoryId=1 },
                new Product { ProductName = "Bose QuietComfort 45 Headphones", ProductDescription= "Noise-canceling headphones with Bluetooth connectivity", CategoryId=3 }
                );
        }
    }
}