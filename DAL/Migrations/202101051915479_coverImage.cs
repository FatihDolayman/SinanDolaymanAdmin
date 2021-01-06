namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class coverImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Interviews", "CoverImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Interviews", "CoverImage");
        }
    }
}
