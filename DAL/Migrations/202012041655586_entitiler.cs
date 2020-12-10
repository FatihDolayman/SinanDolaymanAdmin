namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class entitiler : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Summary", c => c.String());
            AddColumn("dbo.Interviews", "Summary", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Interviews", "Summary");
            DropColumn("dbo.Books", "Summary");
        }
    }
}
