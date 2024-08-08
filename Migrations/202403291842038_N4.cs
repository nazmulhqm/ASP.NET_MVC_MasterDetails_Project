namespace MVCProject_Nazmul.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class N4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "Customer_CustomerId", c => c.Int());
            AddColumn("dbo.Orders", "CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderDetails", "Customer_CustomerId");
            CreateIndex("dbo.Orders", "CustomerId");
            AddForeignKey("dbo.Orders", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
            AddForeignKey("dbo.OrderDetails", "Customer_CustomerId", "dbo.Customers", "CustomerId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "Customer_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.OrderDetails", new[] { "Customer_CustomerId" });
            DropColumn("dbo.Orders", "CustomerId");
            DropColumn("dbo.OrderDetails", "Customer_CustomerId");
        }
    }
}
