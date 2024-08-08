namespace MVCProject_Nazmul.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class N7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "Customer_CustomerId", "dbo.Customers");
            DropIndex("dbo.OrderDetails", new[] { "Customer_CustomerId" });
            DropColumn("dbo.OrderDetails", "Customer_CustomerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "Customer_CustomerId", c => c.Int());
            CreateIndex("dbo.OrderDetails", "Customer_CustomerId");
            AddForeignKey("dbo.OrderDetails", "Customer_CustomerId", "dbo.Customers", "CustomerId");
        }
    }
}
