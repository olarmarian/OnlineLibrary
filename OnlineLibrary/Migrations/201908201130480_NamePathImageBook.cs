namespace OnlineLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NamePathImageBook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Path", c => c.String());
            AddColumn("dbo.Books", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Name");
            DropColumn("dbo.Books", "Path");
        }
    }
}
