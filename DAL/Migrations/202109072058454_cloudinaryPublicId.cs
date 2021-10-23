namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cloudinaryPublicId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "CloudinaryPublicId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "CloudinaryPublicId");
        }
    }
}
