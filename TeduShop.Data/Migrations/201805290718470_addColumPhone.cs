namespace TeduShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColumPhone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContactDetails", "Phone", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContactDetails", "Phone");
        }
    }
}
