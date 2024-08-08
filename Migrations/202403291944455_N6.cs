namespace MVCProject_Nazmul.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class N6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "Order_OrderId", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "Order_OrderId" });
            RenameColumn(table: "dbo.OrderDetails", name: "Order_OrderId", newName: "OrderId");
            AlterColumn("dbo.OrderDetails", "OrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderDetails", "OrderId");
            AddForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders", "OrderId", cascadeDelete: true);
            DropColumn("dbo.OrderDetails", "OrderMasterId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "OrderMasterId", c => c.Int(nullable: false));
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            AlterColumn("dbo.OrderDetails", "OrderId", c => c.Int());
            RenameColumn(table: "dbo.OrderDetails", name: "OrderId", newName: "Order_OrderId");
            CreateIndex("dbo.OrderDetails", "Order_OrderId");
            AddForeignKey("dbo.OrderDetails", "Order_OrderId", "dbo.Orders", "OrderId");
        }
    }
}
