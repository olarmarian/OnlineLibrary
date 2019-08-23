namespace OnlineLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileImageBook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "File", c => c.Binary());
            DropColumn("dbo.Books", "Path");
            DropColumn("dbo.Books", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "Name", c => c.String());
            AddColumn("dbo.Books", "Path", c => c.String());
            DropColumn("dbo.Books", "File");
        }
    }
}
