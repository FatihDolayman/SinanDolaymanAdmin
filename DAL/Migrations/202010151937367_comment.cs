namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ArticleComments", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.BookComments", "BookId", "dbo.Books");
            DropForeignKey("dbo.InterviewComments", "InterviewId", "dbo.Interviews");
            DropForeignKey("dbo.SoundComments", "SoundId", "dbo.Sounds");
            DropForeignKey("dbo.VideoComments", "VideoId", "dbo.Videos");
            DropIndex("dbo.ArticleComments", new[] { "ArticleId" });
            DropIndex("dbo.BookComments", new[] { "BookId" });
            DropIndex("dbo.InterviewComments", new[] { "InterviewId" });
            DropIndex("dbo.SoundComments", new[] { "SoundId" });
            DropIndex("dbo.VideoComments", new[] { "VideoId" });
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        IsOk = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Commenter = c.String(),
                        LikeCount = c.Int(nullable: false),
                        DislikeCount = c.Int(nullable: false),
                        ModuleId = c.Int(nullable: false),
                        Module = c.Int(nullable: false),
                        Article_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.Article_Id)
                .Index(t => t.Article_Id);
            
            DropTable("dbo.ArticleComments");
            DropTable("dbo.BookComments");
            DropTable("dbo.InterviewComments");
            DropTable("dbo.SoundComments");
            DropTable("dbo.VideoComments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.VideoComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        IsOk = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Commenter = c.String(),
                        VideoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SoundComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        IsOk = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Commenter = c.String(),
                        SoundId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InterviewComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        IsOk = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Commenter = c.String(),
                        InterviewId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BookComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        IsOk = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Commenter = c.String(),
                        BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ArticleComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        IsOk = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Commenter = c.String(),
                        ArticleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Comments", "Article_Id", "dbo.Articles");
            DropIndex("dbo.Comments", new[] { "Article_Id" });
            DropTable("dbo.Comments");
            CreateIndex("dbo.VideoComments", "VideoId");
            CreateIndex("dbo.SoundComments", "SoundId");
            CreateIndex("dbo.InterviewComments", "InterviewId");
            CreateIndex("dbo.BookComments", "BookId");
            CreateIndex("dbo.ArticleComments", "ArticleId");
            AddForeignKey("dbo.VideoComments", "VideoId", "dbo.Videos", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SoundComments", "SoundId", "dbo.Sounds", "Id", cascadeDelete: true);
            AddForeignKey("dbo.InterviewComments", "InterviewId", "dbo.Interviews", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BookComments", "BookId", "dbo.Books", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ArticleComments", "ArticleId", "dbo.Articles", "Id", cascadeDelete: true);
        }
    }
}
