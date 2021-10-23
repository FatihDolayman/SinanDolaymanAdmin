namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modif : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "ModifyDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "ModifyDate", c => c.DateTime(nullable: false));
        }
    }
}
