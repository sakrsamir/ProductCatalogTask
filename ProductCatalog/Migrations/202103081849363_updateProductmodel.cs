namespace ProductCatalog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateProductmodel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "LastUpdated", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "LastUpdated", c => c.DateTime(nullable: false));
        }
    }
}
