namespace MVCProject_Nazmul.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class N5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Image", c => c.String());
        }
    }
}
