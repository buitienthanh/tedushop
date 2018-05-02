namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NameUnsigned : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "NameUnsigned", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.Posts", "NameUnsigned", c => c.String(nullable: false, maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "NameUnsigned");
            DropColumn("dbo.Products", "NameUnsigned");
        }
    }
}
