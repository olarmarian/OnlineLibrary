namespace OnlineLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStringFile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "StringFile", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "StringFile");
        }
    }
}
