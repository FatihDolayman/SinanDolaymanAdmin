namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kapakResimleri : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "CoverImage", c => c.String());
            AddColumn("dbo.Books", "CoverImage", c => c.String());
            AddColumn("dbo.Sounds", "CoverImage", c => c.String());
            AddColumn("dbo.Videos", "CoverImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Videos", "CoverImage");
            DropColumn("dbo.Sounds", "CoverImage");
            DropColumn("dbo.Books", "CoverImage");
            DropColumn("dbo.Articles", "CoverImage");
        }
    }
}
