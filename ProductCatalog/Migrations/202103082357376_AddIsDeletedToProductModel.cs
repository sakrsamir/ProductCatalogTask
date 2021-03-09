namespace ProductCatalog.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddIsDeletedToProductModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "IsDeleted", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "IsDeleted");
        }
    }
}
