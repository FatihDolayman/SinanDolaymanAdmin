namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class soundPath : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Sounds", "Audio");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sounds", "Audio", c => c.String());
        }
    }
}
