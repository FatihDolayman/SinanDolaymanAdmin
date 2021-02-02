namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullables : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "ModifyDate", c => c.DateTime());
            AlterColumn("dbo.Sounds", "ModifyDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sounds", "ModifyDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Articles", "ModifyDate", c => c.DateTime(nullable: false));
        }
    }
}
