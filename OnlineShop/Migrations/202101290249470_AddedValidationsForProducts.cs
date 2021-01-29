namespace OnlineShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedValidationsForProducts : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 25));
            AlterColumn("dbo.Products", "Description", c => c.String(nullable: false, maxLength: 90));
            AlterColumn("dbo.Products", "ImageUrl", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "ImageUrl", c => c.String());
            AlterColumn("dbo.Products", "Description", c => c.String());
            AlterColumn("dbo.Products", "Name", c => c.String());
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 8));
        }
    }
}
