namespace MVCProject_Nazmul.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class N3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "ParentCategoryID", "dbo.Categories");
            DropIndex("dbo.Categories", new[] { "ParentCategoryID" });
            DropColumn("dbo.Categories", "ParentCategoryID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "ParentCategoryID", c => c.Int());
            CreateIndex("dbo.Categories", "ParentCategoryID");
            AddForeignKey("dbo.Categories", "ParentCategoryID", "dbo.Categories", "CategoryId");
        }
    }
}
