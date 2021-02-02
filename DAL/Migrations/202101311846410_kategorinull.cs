namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kategorinull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Videos", "CategoryId", "dbo.VideoCategories");
            DropIndex("dbo.Videos", new[] { "CategoryId" });
            AlterColumn("dbo.Videos", "CategoryId", c => c.Int());
            CreateIndex("dbo.Videos", "CategoryId");
            AddForeignKey("dbo.Videos", "CategoryId", "dbo.VideoCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Videos", "CategoryId", "dbo.VideoCategories");
            DropIndex("dbo.Videos", new[] { "CategoryId" });
            AlterColumn("dbo.Videos", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Videos", "CategoryId");
            AddForeignKey("dbo.Videos", "CategoryId", "dbo.VideoCategories", "Id", cascadeDelete: true);
        }
    }
}
