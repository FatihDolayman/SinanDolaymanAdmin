namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullableKategori : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sounds", "CategoryId", "dbo.SoundCategories");
            DropIndex("dbo.Sounds", new[] { "CategoryId" });
            AlterColumn("dbo.Sounds", "CategoryId", c => c.Int());
            CreateIndex("dbo.Sounds", "CategoryId");
            AddForeignKey("dbo.Sounds", "CategoryId", "dbo.SoundCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sounds", "CategoryId", "dbo.SoundCategories");
            DropIndex("dbo.Sounds", new[] { "CategoryId" });
            AlterColumn("dbo.Sounds", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Sounds", "CategoryId");
            AddForeignKey("dbo.Sounds", "CategoryId", "dbo.SoundCategories", "Id", cascadeDelete: true);
        }
    }
}
