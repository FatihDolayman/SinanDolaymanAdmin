namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commentReply : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommentReplies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentId = c.Int(nullable: false),
                        Content = c.String(),
                        IsOk = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Commenter = c.String(),
                        LikeCount = c.Int(nullable: false),
                        DislikeCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CommentReplies");
        }
    }
}
