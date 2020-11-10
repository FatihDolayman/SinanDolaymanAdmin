namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SesYol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sounds", "Path", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sounds", "Path");
        }
    }
}
