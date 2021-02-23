namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seslendirme : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sounds", "Audio", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sounds", "Audio");
        }
    }
}
