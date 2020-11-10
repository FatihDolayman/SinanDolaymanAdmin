namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yazar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Author", c => c.String());
            AddColumn("dbo.Books", "Author", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Author");
            DropColumn("dbo.Articles", "Author");
        }
    }
}
