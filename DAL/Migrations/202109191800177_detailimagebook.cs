namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class detailimagebook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "DetailImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "DetailImage");
        }
    }
}
