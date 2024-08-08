namespace MVCProject_Nazmul.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class N2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "CategoryId" });
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(maxLength: 50),
                        Gender = c.String(),
                        Addresses = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            AddColumn("dbo.Categories", "ParentCategoryID", c => c.Int());
            AlterColumn("dbo.Categories", "CategoryName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Categories", "CategoryDescription", c => c.String(nullable: false, maxLength: 150));
            CreateIndex("dbo.Categories", "ParentCategoryID");
            AddForeignKey("dbo.Categories", "ParentCategoryID", "dbo.Categories", "CategoryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "ParentCategoryID", "dbo.Categories");
            DropIndex("dbo.Categories", new[] { "ParentCategoryID" });
            AlterColumn("dbo.Categories", "CategoryDescription", c => c.String());
            AlterColumn("dbo.Categories", "CategoryName", c => c.String());
            DropColumn("dbo.Categories", "ParentCategoryID");
            DropTable("dbo.Customers");
            CreateIndex("dbo.Products", "CategoryId");
            AddForeignKey("dbo.Products", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
    }
}
